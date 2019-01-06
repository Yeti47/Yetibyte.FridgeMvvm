using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Services {

    public class ServiceProvider : IServiceProvider {

        #region Constants

        private const string ERR_MESSAGE_SERVICE_NULL = "The given service instance is null.";

        #endregion

        #region Fields

        private readonly List<IService> _services = new List<IService>();
        private readonly ReadOnlyCollection<IService> _servicesReadOnly;

        #endregion

        #region Properties

        public IEnumerable<IService> Services => _servicesReadOnly;

        #endregion

        #region Constructors

        public ServiceProvider(IEnumerable<IService> services = null) {

            _servicesReadOnly = new ReadOnlyCollection<IService>(_services);

            if(services != null) {

                foreach (IService service in services)
                    RegisterService(service);

            }

        }

        #endregion

        #region Methods

        public void RegisterService(IService service) => _services.Add(service ?? throw new ServiceRegistrationException(service, ERR_MESSAGE_SERVICE_NULL));

        public bool UnregisterService(IService service) => _services.Remove(service ?? throw new ServiceRegistrationException(service, ERR_MESSAGE_SERVICE_NULL));

        public bool UnregisterService(string serviceId) {

            if (string.IsNullOrWhiteSpace(serviceId))
                throw new ArgumentNullException(nameof(serviceId));
            
            return _services.FirstOrDefault(s => s.ServiceId == serviceId) is IService service && _services.Remove(service);


        }

        public int UnregisterServices(Predicate<IService> predicate) => _services.RemoveAll(predicate ?? throw new ArgumentNullException(nameof(predicate)));

        public int UnregisterServices<TService>(Predicate<TService> predicate = null) where TService : IService {

            predicate = predicate ?? (s => true);

            return _services.RemoveAll(s => s is TService ts && predicate(ts));

        }

        public TService GetService<TService>(string serviceId = null) where TService : IService {

            return _services.OfType<TService>().FirstOrDefault(t => string.IsNullOrWhiteSpace(serviceId) || t.ServiceId == serviceId);

        }
        public IEnumerable<TService> GetServices<TService>() where TService : IService => _services.OfType<TService>();

        public void ClearServices() => _services.Clear();

        #endregion

    }

}

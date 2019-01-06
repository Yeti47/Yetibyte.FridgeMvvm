using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Services {

    public abstract class ServiceBase : IService {

        public virtual string ServiceId { get; protected set; }

        public ServiceBase(string serviceId = null) => ServiceId = serviceId;


    }
}

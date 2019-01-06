using System;
using System.Collections.Generic;

namespace Yetibyte.FridgeMvvm.Services {

    public interface IServiceProvider {

        IEnumerable<IService> Services { get; }

        void ClearServices();
        TService GetService<TService>(string serviceId = null) where TService : IService;
        IEnumerable<TService> GetServices<TService>() where TService : IService;
        void RegisterService(IService service);
        bool UnregisterService(IService service);
        bool UnregisterService(string serviceId);
        int UnregisterServices(Predicate<IService> predicate);
        int UnregisterServices<TService>(Predicate<TService> predicate = null) where TService : IService;

    }

}
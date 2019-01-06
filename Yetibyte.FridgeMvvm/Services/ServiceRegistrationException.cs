using Yetibyte.FridgeMvvm.Services;
using System;

namespace Yetibyte.FridgeMvvm.Services {

    public class ServiceRegistrationException : Exception {

        private const string DEFAULT_ERR_MESSAGE = "The given service could not be registered.";

        /// <summary>
        /// A reference to the <see cref="IService"/> whose registration caused the exception.
        /// </summary>
        public IService Service { get; }

        public ServiceRegistrationException(IService service, string message = DEFAULT_ERR_MESSAGE) : base(message) {

            Service = service;

        }

    }

}

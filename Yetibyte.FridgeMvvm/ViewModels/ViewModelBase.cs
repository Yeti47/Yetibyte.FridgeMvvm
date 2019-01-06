using Yetibyte.FridgeMvvm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yetibyte.FridgeMvvm.Core;

namespace Yetibyte.FridgeMvvm.ViewModels {

    public abstract class ViewModelBase : ObservableObject {

        #region Properties

        public Services.IServiceProvider ServiceProvider { get; }

        #endregion

        #region Constructors

        protected ViewModelBase(Services.IServiceProvider serviceProvider = null) {

            ServiceProvider = serviceProvider ?? new ServiceProvider();

        }

        #endregion

    }

}

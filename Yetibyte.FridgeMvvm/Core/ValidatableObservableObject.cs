using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Core {

    public abstract class ValidatableObservableObject : ObservableObject {

        #region Properties

        public abstract bool IsValid { get; }

        #endregion

        #region Events

        public event EventHandler<IsValidChangedEventArgs> IsValidChanged;

        #endregion

        #region Methods

        protected virtual bool SetPropertyWithValidation<T>(T value, [CallerMemberName] string propertyName = "") {

            T temp = default(T);

            return SetPropertyWithValidation(ref temp, value, propertyName, true);

        }

        protected virtual bool SetPropertyWithValidation<T>(ref T field, T value, [CallerMemberName] string propertyName = "") => SetPropertyWithValidation(ref field, value, propertyName, false);

        private bool SetPropertyWithValidation<T>(ref T field, T value, string propertyName, bool useReflection) {

            bool wasValid = IsValid;

            if (useReflection)
                SetProperty(value, propertyName);
            else
                SetProperty(ref field, value, propertyName);

            bool isValid = IsValid;

            if (wasValid != isValid) {

                OnPropertyChanged(nameof(IsValid));
                OnIsValidChanged(new IsValidChangedEventArgs(isValid, propertyName, value));

            }
            
            return wasValid != isValid;

        }

        protected virtual void OnIsValidChanged(IsValidChangedEventArgs eventArgs) {

            EventHandler<IsValidChangedEventArgs> handler = IsValidChanged;
            handler?.Invoke(this, eventArgs);

        }

        #endregion

    }
}

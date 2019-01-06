using System;

namespace Yetibyte.FridgeMvvm.Core {

    public class IsValidChangedEventArgs : EventArgs {

        public bool IsValid { get; }
        public string PropertyName { get; }
        public object PropertyValue { get; }

        public IsValidChangedEventArgs(bool isValid, string propertyName, object propertyValue) {

            IsValid = isValid;
            PropertyName = propertyName;
            PropertyValue = propertyValue;

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Yetibyte.FridgeMvvm.Utilities;

namespace Yetibyte.FridgeMvvm.Core {

    public abstract class ObservableObject : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") {

            PropertyChangedEventHandler handler = PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "") {

            if ((typeof(T).IsValueType && !field.Equals(value)) || (!typeof(T).IsValueType && !ReferenceEquals(field, value))) {

                field = value;
                OnPropertyChanged(propertyName);

            }
        }

        protected void SetProperty<T>(T value, [CallerMemberName] string propertyName = "") {

            IEnumerable<FieldInfo> fields = ReflectionUtil.FindFieldsWithAttribute<ObservablePropertyBackingFieldAttribute>(this);

            FieldInfo targetField = fields?.FirstOrDefault(f => f.FieldType == typeof(T) && f.GetCustomAttribute<ObservablePropertyBackingFieldAttribute>().PropertyName == propertyName);

            if (targetField == null)
                throw new Exception($"Backing field for the property '{propertyName}' with matching type could not be found.");

            T fieldVal = (T)targetField.GetValue(this);

            if((targetField.FieldType.IsValueType && !fieldVal.Equals(value)) || (!targetField.FieldType.IsValueType && !ReferenceEquals(fieldVal, value))) {

                targetField.SetValue(this, value);
                OnPropertyChanged(propertyName);

            }
        }

    }

}

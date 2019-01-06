using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Core {

    public class ObservablePropertyBackingFieldAttribute : Attribute {

        private const string ERR_PROPERTY_NAME_BLANK = "The property name must not be null or blank.";

        public string PropertyName { get; }

        public ObservablePropertyBackingFieldAttribute(string propertyName) {
            
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName), ERR_PROPERTY_NAME_BLANK);

        }

        public override string ToString() => $"PropertyName: {PropertyName}";

    }

}

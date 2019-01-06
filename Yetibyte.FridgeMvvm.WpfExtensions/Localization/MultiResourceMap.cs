using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace Yetibyte.FridgeMvvm.WpfExtensions.Localization {

    public class MultiResourceMap : ResourceMapBase {

        #region Constants

        public const string DEFAULT_KEY_SEPARATOR = "::";

        #endregion

        #region Indexers

        public string this[MultiResourceMapKey resourceKey] => GetLocalizedValue(resourceKey);

        public string this[string resourceName, string resourceKey] => GetLocalizedValue(resourceName, resourceKey);

        public string this[Type resourceType, string resourceKey] => GetLocalizedValue(resourceType?.Name, resourceKey);

        #endregion

        #region Dependency Properties

        #endregion

        #region Fields

        private string _keySeparator = DEFAULT_KEY_SEPARATOR;

        #endregion

        #region Properties

        public ResourceTypeCollection ResourceTypes { get; } = new ResourceTypeCollection();

        public string KeySeparator {
            get => _keySeparator;
            set => _keySeparator = string.IsNullOrWhiteSpace(value) ? DEFAULT_KEY_SEPARATOR : value;
        }

        #endregion

        #region Constructors

        public MultiResourceMap() {
            
        }

        public MultiResourceMap(IEnumerable<Type> resourceTypes, CultureInfo currentCulture = null, CultureInfo defaultCulture = null) : base(currentCulture, defaultCulture) {

            ResourceTypes.AddRange(resourceTypes ?? new Type[0]);

        }

        #endregion

        #region Methods

        public override string GetLocalizedValue(string resourceKey) {

            if (string.IsNullOrWhiteSpace(resourceKey))
                throw new ArgumentNullException(nameof(resourceKey), "The resource key must not be null or blank.");

            return GetLocalizedValue(MultiResourceMapKey.Parse(resourceKey, _keySeparator));

        }

        public string GetLocalizedValue(string resourceName, string resourceKey) {

            // if the resourceName is omitted, look for first occurrence of the resourceKey in all resources
            if(string.IsNullOrWhiteSpace(resourceName)) {

                string resourceValue = null;

                foreach(Type rt in ResourceTypes) {

                    resourceValue = new ResourceManager(rt).GetString(resourceKey, EffectiveCulture);

                    if (resourceValue != null)
                        break;
                    
                }

                return resourceValue;

            }

            Type resType = ResourceTypes.FindResourceType(resourceName);

            return resType != null ? new ResourceManager(resType).GetString(resourceKey, EffectiveCulture) : null;

        }

        public string GetLocalizedValue(MultiResourceMapKey resourceKey) {

            if (resourceKey == null)
                throw new ArgumentNullException(nameof(resourceKey));

            return GetLocalizedValue(resourceKey.ResourceName, resourceKey.Key);

        }

        #endregion

    }

}

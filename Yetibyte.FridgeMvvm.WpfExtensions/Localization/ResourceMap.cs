using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace Yetibyte.FridgeMvvm.WpfExtensions.Localization {

    /// <summary>
    /// Acts as an observable wrapper around a <see cref="ResourceManager"/> for a specific resource type. A resource map is typically defined as a static resource in either the view's or the application's XAML code.
    /// </summary>
    public class ResourceMap : ResourceMapBase {

        #region Fields

        private Type _resourceType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of the resource class to use for looking up localized resource values.
        /// </summary>
        public Type ResourceType {
            get => _resourceType;
            set {
                
                if(value == null)
                    throw new ArgumentNullException(nameof(value), "The resource type must not be null.");

                if(_resourceType != value) {

                    _resourceType = value;
                    OnPropertyChanged();
                    OnPropertyChanged(INDEXER_PROPERTY_NAME);

                }

            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceMap"/> class. This default constructor is provided in order to facilitate creating resource maps in XAML mark-up.
        /// When creating a resource map programmatically, it is recommended to use one of the parameterized constructors to ensure that the new instance is initialized properly.
        /// In XAML, always make sure that the <see cref="ResourceType"/> property is set.
        /// </summary>
        public ResourceMap() {

        }

        public ResourceMap(Type resourceType, CultureInfo currentCulture = null, CultureInfo defaultCulture = null) : base(currentCulture, defaultCulture) {

            ResourceType = resourceType;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the resource value of the resource with the given key. If the resource could not be retrieved, null is returned.
        /// </summary>
        /// <param name="resourceKey">The key of the resource to retrieve.</param>
        /// <returns>The value of the resource or null if it could not be found.</returns>
        public override string GetLocalizedValue(string resourceKey) {

            if (string.IsNullOrWhiteSpace(resourceKey))
                return null;

            if (_resourceType == null)
                throw new InvalidOperationException("Cannot retrieve resource value since a resource type has not been provided.");
            
            ResourceManager resourceManager = new ResourceManager(_resourceType);
            
            return resourceManager.GetString(resourceKey, EffectiveCulture);
            
        }

        #endregion
        
    }
}

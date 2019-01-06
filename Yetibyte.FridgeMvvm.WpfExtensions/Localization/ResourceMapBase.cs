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
using Yetibyte.FridgeMvvm.Utilities;
using Yetibyte.FridgeMvvm.Services;
using Yetibyte.FridgeMvvm.Core;

namespace Yetibyte.FridgeMvvm.WpfExtensions.Localization {

    public abstract class ResourceMapBase : DependencyObject, INotifyPropertyChanged, ILocalizationService {

        #region Constants

        /// <summary>
        /// The property name that is used in order to indicate that the return value of the indexer operator [] has changed.
        /// </summary>
        protected const string INDEXER_PROPERTY_NAME = "Item[]";

        #endregion

        #region Indexers

        /// <summary>
        /// Returns the resource value of the resource with the given key. If the resource could not be retrieved, null is returned.
        /// </summary>
        /// <param name="resourceKey">The key of the resource to retrieve.</param>
        /// <returns>The value of the resource or null if it could not be found.</returns>
        public virtual string this[string resourceKey] => GetLocalizedValue(resourceKey);

        #endregion

        #region Fields

        private CultureInfo _defaultCulture;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty CurrentCultureProperty = DependencyProperty.Register(
            nameof(CurrentCultureProperty).RemoveDependencyPropertySuffix(), 
            typeof(CultureInfo), 
            typeof(ResourceMapBase), 
            new PropertyMetadata(null, (d, v) => (d as ResourceMapBase)?.OnPropertyChanged(INDEXER_PROPERTY_NAME), (d, v) => v ?? (d as ResourceMapBase)?.DefaultCulture));

        #endregion

        #region Properties

        public string ServiceId { get; set; }

        /// <summary>
        /// The culture that is effectively used for looking up resources. 
        /// </summary>
        public CultureInfo EffectiveCulture => CurrentCulture ?? DefaultCulture;

        /// <summary>
        /// The culture to use if the <see cref="CurrentCulture"/> could not be retrieved. This also determines the culture used during design mode.
        /// If a default culture was not set explicitely, the current thread's UI culture will be returned.
        /// </summary>
        public CultureInfo DefaultCulture {
            get => _defaultCulture ?? Thread.CurrentThread.CurrentUICulture;
            set => _defaultCulture = value;
        }

        /// <summary>
        /// The culture currently used for looking up resources. This may return null if this <see cref="ResourceMap"/> was instantiated using the default constructor. 
        /// This is to allow fallback to the <see cref="DefaultCulture"/> during design mode. However, if an attempt is made to set this property to null directly, 
        /// this property will be assigned the current <see cref="DefaultCulture"/>.
        /// </summary>
        public CultureInfo CurrentCulture {
            get => (CultureInfo)GetValue(CurrentCultureProperty);
            set => SetValue(CurrentCultureProperty, value);
        }

        /// <summary>
        /// If this <see cref="ResourceMap"/>s CurrentCulture property was data-bound to a certain source using one of the overloads of the <see cref="BindToCultureProvider"/> method, this stores a reference to
        /// the binding expression used. Otherwise this returns null.
        /// </summary>
        public BindingExpressionBase CultureBinding { get; private set; }

        /// <summary>
        /// Returns true if this <see cref="ResourceMap"/>s CurrentCulture property uses an active data-binding.
        /// </summary>
        public bool HasActiveCultureBinding => CultureBinding != null && CultureBinding.Status == BindingStatus.Active;

        #endregion

        #region Static Properties

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        
        protected ResourceMapBase() {
            
        }

        protected ResourceMapBase(CultureInfo currentCulture, CultureInfo defaultCulture = null) {
            
            _defaultCulture = defaultCulture;
            CurrentCulture = currentCulture;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property whose value changed. If omitted, the name of the calling member will be used.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {

            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event and passes an empty string as the argument to indicate that all properties have changed.
        /// </summary>
        protected virtual void RaiseAllPropertiesChanged() => OnPropertyChanged(string.Empty);

        /// <summary>
        /// Returns the resource value of the resource with the given key. If the resource could not be retrieved, null is returned.
        /// </summary>
        /// <param name="resourceKey">The key of the resource to retrieve.</param>
        /// <returns>The value of the resource or null if it could not be found.</returns>
        public abstract string GetLocalizedValue(string resourceKey);

        public BindingExpressionBase BindToCultureProvider(ICultureProvider cultureProvider) {

            if (cultureProvider == null)
                throw new ArgumentNullException(nameof(cultureProvider));
            
            CultureBinding = BindingOperations.SetBinding(this, CurrentCultureProperty, new Binding(nameof(ICultureProvider.Culture)) { Source = cultureProvider });
            CultureBinding.UpdateTarget();

            return CultureBinding;

        }

        /// <summary>
        /// Tries to bind the <see cref="CurrentCulture"/> property to the given source. If the binding could be created successfully and without any binding or validation errors, the newly created binding expression
        /// will be assigned to the <see cref="CultureBinding"/> property and returned by this method. Otherwise, the current value of the <see cref="CultureBinding"/> property remains unchanged and null is returned.
        /// </summary>
        /// <param name="cultureProvider">The source of the data binding.</param>
        /// <param name="propertyPath">The path of the property to bind this object's current culture to. The underlying property must be of type <see cref="CultureInfo"/>. Otherwise, the binding will fail and null will be returned.</param>
        /// <returns>The newly created binding expression if no errors occurred; null otherwise.</returns>
        public BindingExpressionBase BindToCultureProvider(object cultureProvider, string propertyPath) {

            if (cultureProvider == null)
                throw new ArgumentNullException(nameof(cultureProvider));

            if (string.IsNullOrWhiteSpace(propertyPath))
                throw new ArgumentNullException(nameof(propertyPath), "A valild property path must be provided.");

            BindingExpressionBase originalBindingExpression = CultureBinding;

            BindingExpressionBase bindingExpression = BindingOperations.SetBinding(this, CurrentCultureProperty, new Binding(propertyPath) { Source = cultureProvider });

            if (bindingExpression != null && !bindingExpression.HasError) {

                CultureBinding = bindingExpression;
                bindingExpression.UpdateTarget();

            }
            else {

                ClearCultureBinding();
                CultureBinding = BindingOperations.SetBinding(this, CurrentCultureProperty, originalBindingExpression.ParentBindingBase);

                return null;

            }

            return bindingExpression;

        }

        /// <summary>
        /// Removes the data-binding from the <see cref="CurrentCulture"/> property.
        /// </summary>
        public void ClearCultureBinding() {

            BindingOperations.ClearBinding(this, CurrentCultureProperty);
            CultureBinding = null;

        }

        #endregion
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Yetibyte.FridgeMvvm.Core;
using Yetibyte.FridgeMvvm.WpfExtensions.Localization;

namespace Yetibyte.FridgeMvvm.WpfExtensions.Utilities
{
    public static class FrameworkElementExtensions { 

        public static bool BindCultureToResourceMap(this FrameworkElement frameworkElement, object key, ICultureProvider cultureProvider) {

            if (BindCultureExceptionCheck(frameworkElement, key, cultureProvider) is Exception ex)
                throw ex;

            return frameworkElement.Resources[key] is ResourceMapBase resourceMap && !resourceMap.BindToCultureProvider(cultureProvider).HasError;

        }

        public static bool BindCultureToResourceMap(this FrameworkElement frameworkElement, object key, object cultureProvider, string propertyPath) {

            if (BindCultureExceptionCheck(frameworkElement, key, cultureProvider, propertyPath) is Exception ex)
                throw ex;

            return frameworkElement.Resources[key] is ResourceMapBase resourceMap && !resourceMap.BindToCultureProvider(cultureProvider, propertyPath).HasError;

        }

        public static void BindCultureToAllResourceMaps(this FrameworkElement frameworkElement, ICultureProvider cultureProvider) {

            if (frameworkElement == null)
                throw new ArgumentNullException(nameof(frameworkElement));

            if (cultureProvider == null)
                throw new ArgumentNullException(nameof(cultureProvider));

            foreach (ResourceMapBase resourceMap in frameworkElement.Resources.OfType<ResourceMapBase>())
                resourceMap.BindToCultureProvider(cultureProvider);

        }

        public static void BindCultureToAllResourceMaps(this FrameworkElement frameworkElement, object cultureProvider, string propertyPath) {

            if (frameworkElement == null)
                throw new ArgumentNullException(nameof(frameworkElement));

            if (cultureProvider == null)
                throw new ArgumentNullException(nameof(cultureProvider));

            if (string.IsNullOrWhiteSpace(propertyPath))
                throw new ArgumentNullException(nameof(propertyPath));

            foreach (ResourceMapBase resourceMap in frameworkElement.Resources.OfType<ResourceMapBase>())
                resourceMap.BindToCultureProvider(cultureProvider, propertyPath);

        }

        private static Exception BindCultureExceptionCheck(FrameworkElement frameworkElement, object key, object cultureProvider) {

            Exception exception = null;

            if (frameworkElement == null)
                exception = new ArgumentNullException(nameof(frameworkElement));

            else if (key == null || (key is string str && str.Trim() == string.Empty))
                exception = new ArgumentNullException(nameof(key));

            else if (cultureProvider == null)
                exception = new ArgumentNullException(nameof(cultureProvider));

            return exception;

        }

        private static Exception BindCultureExceptionCheck(this FrameworkElement frameworkElement, object key, object cultureProvider, string propertyPath) {

            Exception exception = BindCultureExceptionCheck(frameworkElement, key, cultureProvider);

            if (exception == null && string.IsNullOrWhiteSpace(propertyPath))
                exception = new ArgumentNullException(propertyPath);

            return exception;

        }
        
    }

}

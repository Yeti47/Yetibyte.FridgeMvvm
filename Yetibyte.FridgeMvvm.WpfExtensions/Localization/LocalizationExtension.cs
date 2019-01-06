using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Yetibyte.FridgeMvvm.WpfExtensions.Localization
{
    public class LocalizationExtension : MarkupExtension {

        [ConstructorArgument("resourceMap")]
        public ResourceMapBase ResourceMap { get; set; }

        [ConstructorArgument("key")]
        public string Key { get; set; }

        public LocalizationExtension() {

        }

        public LocalizationExtension(ResourceMapBase resourceMap, string key) {

            ResourceMap = resourceMap;
            Key = key;

        }

        public override object ProvideValue(IServiceProvider serviceProvider) {

            if (ResourceMap == null || string.IsNullOrWhiteSpace(Key))
                return null;

            string bindingPath = $"[{Key}]";

            Binding binding = new Binding {
                Source = ResourceMap,
                Path = new PropertyPath(bindingPath),
                Mode = BindingMode.OneWay
            };
            
            return binding.ProvideValue(serviceProvider);

        }

    }
}

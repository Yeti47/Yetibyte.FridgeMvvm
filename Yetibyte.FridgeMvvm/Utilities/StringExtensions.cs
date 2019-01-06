using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yetibyte.FridgeMvvm.Utilities
{
    public static class StringExtensions {

        public const string DEPENDENCY_PROPERTY_SUFFIX = "Property";

        public static string RemoveSuffix(this string source, string suffix) {

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return (string.IsNullOrEmpty(suffix) || !source.EndsWith(suffix)) ? source : source.Substring(0, source.Length - suffix.Length);

        }

        public static string RemovePrefix(this string source, string prefix) {

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return (string.IsNullOrEmpty(prefix) || !source.StartsWith(prefix)) ? source : source.Substring(prefix.Length);

        }

        public static string RemoveDependencyPropertySuffix(this string source) => source?.RemoveSuffix(DEPENDENCY_PROPERTY_SUFFIX) ?? throw new ArgumentNullException(nameof(source));

    }
}

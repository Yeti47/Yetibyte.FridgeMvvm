using System;

namespace Yetibyte.FridgeMvvm.WpfExtensions.Localization {

    public class MultiResourceMapKey {

        public string ResourceName { get; }
        public string Key { get; }

        public bool HasResourceName => !string.IsNullOrWhiteSpace(ResourceName);

        public MultiResourceMapKey(string key, string resourceName = null) {

            Key = !string.IsNullOrWhiteSpace(key) ? key : throw new ArgumentNullException(nameof(key));
            ResourceName = resourceName;

        }

        public MultiResourceMapKey(string key, Type resourceType) : this(key, resourceType.Name) {

        }

        public override string ToString() => $"{(HasResourceName ? "ResourceName: " + ResourceName + " | " : string.Empty)}Key: {Key}";

        public string GetFullKey(string separator = MultiResourceMap.DEFAULT_KEY_SEPARATOR) => $"{(HasResourceName ? ResourceName + separator : string.Empty)}{Key}";

        public static MultiResourceMapKey Parse(string fullKey, string separator = MultiResourceMap.DEFAULT_KEY_SEPARATOR) {

            if (string.IsNullOrWhiteSpace(fullKey))
                return null;

            string resourceName = null;
            string key = null;

            string fullKeyTrimmed = fullKey.Trim();

            int indexFirstSeparator = fullKeyTrimmed.IndexOf(separator);

            if (indexFirstSeparator <= 0 || indexFirstSeparator == fullKeyTrimmed.Length - separator.Length) {

                // this means there is either no separator at all, or it's at the very beginning/end of the full key,
                // in which case it will be considered part of the actual resource key and the resource name is assumed to be omitted
                key = fullKeyTrimmed;

            }
            else {

                resourceName = fullKeyTrimmed.Substring(0, indexFirstSeparator);
                key = fullKeyTrimmed.Substring(indexFirstSeparator + separator.Length);

            }

            return new MultiResourceMapKey(key, resourceName);

        }

    }

}

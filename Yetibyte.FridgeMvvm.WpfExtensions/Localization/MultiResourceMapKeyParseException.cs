using System;

namespace Yetibyte.FridgeMvvm.WpfExtensions.Localization {

    public class MultiResourceMapKeyParseException : Exception {

        private const string DEFAULT_MESSAGE = "The given string could not be parsed to a valid key";

        public string Key { get; }

        public MultiResourceMapKeyParseException(string key, string message = DEFAULT_MESSAGE) : base(message) => Key = key;

    }

}

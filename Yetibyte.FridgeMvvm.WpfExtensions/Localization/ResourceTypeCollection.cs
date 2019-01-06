using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Yetibyte.FridgeMvvm.WpfExtensions.Localization {

    public class ResourceTypeCollection : IList<Type>, IList {

        #region Constants

        private const string ERR_MESSAGE_DUPLICATE_RESOURCE_TYPE = "This ResourceTypeCollection already contains the given type.";
        private const string ERR_MESSAGE_ITEM_NULL = "Cannot add a null reference to the ResourceTypeCollection.";
        private const string ERR_TYPE_MISMATCH = "The given object must be of the type 'Type'.";

        #endregion

        #region Indexers

        public Type this[int index] {
            get => IsIndexInRange(index) ? _resouceTypes[index] : throw new IndexOutOfRangeException();
            set {

                if (value == null)
                    throw new ArgumentNullException(nameof(value), ERR_MESSAGE_ITEM_NULL);

                _resouceTypes[index] = !_resouceTypes.Contains(value) || _resouceTypes[index] == value ? value : throw new ArgumentException(ERR_MESSAGE_DUPLICATE_RESOURCE_TYPE);
            }
        }

        object IList.this[int index] {
            get => this[index];
            set {

                if (value != null && !(value is Type))
                    throw new ArgumentException(ERR_TYPE_MISMATCH);

                this[index] = value as Type;

            }
        }

        #endregion

        #region Fields

        private readonly List<Type> _resouceTypes = new List<Type>();

        #endregion

        #region Properties

        public int Count => _resouceTypes.Count;

        public bool IsReadOnly => false;

        bool IList.IsReadOnly => false;

        bool IList.IsFixedSize => false;

        int ICollection.Count => Count;

        object ICollection.SyncRoot => ((IList)_resouceTypes).SyncRoot;

        bool ICollection.IsSynchronized => ((IList)_resouceTypes).IsSynchronized;

        #endregion

        #region Constructors

        public ResourceTypeCollection() : this(null) {

        }

        public ResourceTypeCollection(IEnumerable<Type> resourceTypes) {
            
            if (resourceTypes != null)
                AddRange(resourceTypes);

        }
        #endregion

        #region Methods

        public void AddRange(IEnumerable<Type> resourceTypes) {

            if (resourceTypes == null)
                throw new ArgumentNullException(nameof(resourceTypes));

            foreach (Type resType in resourceTypes)
                Add(resType);

        }

        public void Add(Type item) {
            
            if (item == null)
                throw new ArgumentNullException(nameof(item), ERR_MESSAGE_ITEM_NULL);

            if (Contains(item))
                throw new ArgumentException(ERR_MESSAGE_DUPLICATE_RESOURCE_TYPE);

            _resouceTypes.Add(item);

        }

        public void Clear() => _resouceTypes.Clear();

        public bool Contains(Type item) => item != null && _resouceTypes.Contains(item);

        public void CopyTo(Type[] array, int arrayIndex) => _resouceTypes.CopyTo(array, arrayIndex);

        public IEnumerator<Type> GetEnumerator() => _resouceTypes.GetEnumerator();

        public int IndexOf(Type item) => _resouceTypes.IndexOf(item);

        public void Insert(int index, Type item) {

            if (!IsIndexInRange(index))
                throw new IndexOutOfRangeException();

            if (item == null)
                throw new ArgumentNullException(nameof(item), ERR_MESSAGE_ITEM_NULL);

            if (Contains(item))
                throw new ArgumentException(ERR_MESSAGE_DUPLICATE_RESOURCE_TYPE);

            _resouceTypes.Insert(index, item);

        }

        public bool Remove(Type item) => _resouceTypes.Remove(item);

        public void RemoveAt(int index) {

            if (!IsIndexInRange(index))
                throw new IndexOutOfRangeException();

            _resouceTypes.RemoveAt(index);

        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_resouceTypes).GetEnumerator();

        private bool IsIndexInRange(int index) => index >= 0 && index < _resouceTypes.Count;

        public Type FindResourceType(string typeName) {

            if (string.IsNullOrWhiteSpace(typeName))
                return null;

            // First look for fully qualified name in case there are multiple types with the same name, but in in different namespace.
            return _resouceTypes.FirstOrDefault(t => t.FullName == typeName) ?? _resouceTypes.FirstOrDefault(t => t.Name == typeName);

        }

        int IList.Add(object value) {

            Type typeItem = value as Type;

            if (typeItem == null)
                throw new ArgumentException(ERR_TYPE_MISMATCH);

            return Contains(typeItem) ? -1 : ((IList)_resouceTypes).Add(typeItem);

        }

        bool IList.Contains(object value) => value is Type && ((IList)_resouceTypes).Contains(value);

        void IList.Clear() => Clear();

        int IList.IndexOf(object value) => value is Type ? ((IList)_resouceTypes).IndexOf(value) : -1;

        void IList.Insert(int index, object value) {

            Type typeItem = value as Type;

            if (typeItem == null)
                throw new ArgumentException(ERR_TYPE_MISMATCH);

            Insert(index, typeItem);

        }

        void IList.Remove(object value) => Remove(value as Type);

        void IList.RemoveAt(int index) => _resouceTypes.RemoveAt(index);

        void ICollection.CopyTo(Array array, int index) => ((IList)_resouceTypes).CopyTo(array, index);

        #endregion

    }

}

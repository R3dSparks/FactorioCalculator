using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Entities.Helpers
{
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyPropertyChanged, INotifyCollectionChanged
    {

        #region Private Variable

        private List<TKey> m_keys;

        private List<TValue> m_values;

        #endregion

        public TValue this[TKey key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Get Key list
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                if (m_keys == null)
                    m_keys = new List<TKey>();

                return m_keys;
            }
        }

        /// <summary>
        /// Get Value list
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                if (m_values == null)
                    m_values = new List<TValue>();

                return m_values;
            }
        }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ObservableDictionary()
        {

        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}

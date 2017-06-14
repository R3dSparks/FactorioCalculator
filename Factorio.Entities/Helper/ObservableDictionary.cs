using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Factorio.Entities.Helper
{
    public class ObservableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        #region Private Values

        private List<KeyValuePair<TKey, TValue>> m_content;

        #endregion

        #region Public Properties



        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ObservableDictionary()
        {
            m_content = new List<KeyValuePair<TKey, TValue>>();
        }

        #endregion

        #region IDictionary

        /// <summary>
        /// Get value for specific key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                return m_content.Find(x => x.Key.Equals(key)).Value;
            }

            set
            {
                int index = m_content.FindIndex(x => x.Key.Equals(key));

                m_content[index] = new KeyValuePair<TKey, TValue>(key, value);
            }
        }

        /// <summary>
        /// Get collection of all keys
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return m_content.Select(x => x.Key).ToList();
            }
        }

        /// <summary>
        /// Get collection of all values
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return m_content.Select(x => x.Value).ToList();
            }
        }

        public int Count => m_content.Count;

        public bool IsReadOnly => false;

        /// <summary>
        /// Add a key and value to the <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            var keyValuePair = new KeyValuePair<TKey, TValue>(key, value);

            m_content.Add(keyValuePair);

            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, keyValuePair));
        }

        /// <summary>
        /// Add a <see cref="KeyValuePair{TKey, TValue}"/> to the <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            m_content.Add(item);

            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }
        
        /// <summary>
        /// Clear contents
        /// </summary>
        public void Clear()
        {
            m_content.Clear();
        }

        /// <summary>
        /// Returns if a <see cref="KeyValuePair{TKey, TValue}"/> is in this <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return m_content.Contains(item);
        }

        /// <summary>
        /// Returns if a key is in this <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return m_content.Select(x => x.Key).Contains(key);
        }

        
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            // TODO Implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the enumerator for this <see cref="ObservableDictionary{TKey, TValue}"/>
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var keyValuePair in m_content)
            {
                yield return keyValuePair;
            }
        }

        /// <summary>
        /// Removes the first occurance of key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            var keyValuePair = m_content.Find(x => x.Key.Equals(key));

            int index = m_content.IndexOf(keyValuePair);

            bool success = m_content.Remove(keyValuePair);

            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, keyValuePair, index));

            return success;
        }

        /// <summary>
        /// Removes a specific <see cref="KeyValuePair{TKey, TValue}"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = m_content.IndexOf(item);

            bool success = m_content.Remove(item);

            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));

            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            var keyValuePair = m_content.Find(x => x.Key.Equals(key));

            if (keyValuePair.Equals(null) == false)
            {
                value = keyValuePair.Value;
                return true;
            }

            value = default(TValue);
            return false;

            

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var keyValuePair in m_content)
            {
                yield return keyValuePair;
            }
        }

        #endregion

        #region INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged = (sender, e) => { };

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        #endregion

    }
}

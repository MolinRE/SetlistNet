using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SetlistNet.Models
{
    /// <summary>
    /// This is an abstract class, that represents a set of items, returned by API
    /// </summary>
    public abstract class ApiArrayResult<T> : IList<T>
    {
        #region Private Fields
        private int _total;
        private int _page;
        private int _itemsPerPage;
        private string _apiType;
        protected List<T> _items;
        #endregion

        /// <summary>
        /// Gets or sets the total amount of items matching the query.
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public int Total
        {
            get
            {
                return this._total;
            }
            set
            {
                this._total = value;
            }
        }
        /// <summary>
        /// Gets or sets current page.
        /// </summary>
        [JsonProperty(PropertyName = "page")]
        public int Page
        {
            get
            {
                return this._page;
            }
            set
            {
                this._page = value;
            }
        }
        /// <summary>
        /// Gets or sets the amount of items you get per page.
        /// </summary>
        [JsonProperty(PropertyName = "itemsPerPage")]
        public int ItemsPerPage
        {
            get
            {
                return this._itemsPerPage;
            }
            set
            {
                this._itemsPerPage = value;
            }
        }

        /// <summary>
        /// Gets or sets the property "type" of an object.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string ApiType
        {
            get { return _apiType; }
            set { _apiType = value; }
        }

        /// <summary>
        /// Gets the total amount of pages returned by API.
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (ItemsPerPage == 0) return 0;
                else
                    if (ItemsPerPage > Total) return 1;
                else
                    return (int)Math.Floor((double)Total / ItemsPerPage);
            }
        }


        public ApiArrayResult()
        {
            _items = new List<T>();
        }

        #region Interface implementation
        #region ICollection<T>
        /// <summary>
        /// Gets Count property of inner list.
        /// </summary>
        public int Count
        {
            get
            {
                return _items == null ? 0 : _items.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return ((IList)_items).IsReadOnly;
            }
        }

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }
        #endregion

        #region IEnumerbale
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion

        #region IEnumerable<T>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (T artist in _items)
                yield return artist;
        }
        #endregion

        #region IList<T>
        public T this[int key]
        {
            get
            {
                return _items[key];
            }
            set
            {
                _items[key] = value;
            }
        }

        public int IndexOf(T item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }
        #endregion
        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This is an abstract class, that represents a set of items, returned by API
    /// </summary>
    public abstract class ApiArrayResult<T> : IList<T>
    {
        protected List<T> _items;

        /// <summary>
        /// Gets or sets the total amount of items matching the query.
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets current page.
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the amount of items you get per page.
        /// </summary>
        [JsonPropertyName("itemsPerPage")]
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the property "type" of an object.
        /// </summary>
        [JsonPropertyName("type")]
        public string ApiType { get; set; }

        /// <summary>
        /// Gets the total amount of pages returned by API.
        /// </summary>
        public int TotalPages
        {
            get
            {
                if (ItemsPerPage == 0)
                {
                    return 0;
                }

                if (ItemsPerPage > Total)
                {
                    return 1;
                }

                return (int)Math.Floor((double)Total / ItemsPerPage);
            }
        }


        public ApiArrayResult()
        {
            _items = new();
        }

        /// <summary>
        /// Gets Count property of inner list.
        /// </summary>
        public int Count => _items.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item) => _items.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item) => _items.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var artist in _items)
            {
                yield return artist;
            }
        }

        public T this[int key]
        {
            get => _items[key];
            set => _items[key] = value;
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
    }
}

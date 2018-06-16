using System.Collections.Generic;
using Newtonsoft.Json;

namespace SetlistNet.Models
{
    [JsonObject]
    /// <summary>
    /// A Result consisting of a list of countries.
    /// </summary>
    public class Countries : ApiArrayResult<Country>
    {
        /// <summary>
        /// Gets or sets the list of countries.
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        internal List<Country> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Count = {0}", Items == null ? 0 : Items.Count);
        }
    }
}

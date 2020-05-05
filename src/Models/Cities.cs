using System.Collections.Generic;
using Newtonsoft.Json;

namespace SetlistNet.Models
{
    [JsonObject]
    /// <summary>
    /// A Result consisting of a list of cities.
    /// </summary>
    public class Cities : ApiArrayResult<City>
    {
        /// <summary>
        /// Gets or sets the list of cities.
        /// </summary>
        [JsonProperty(PropertyName = "cities")]
        internal List<City> Items
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

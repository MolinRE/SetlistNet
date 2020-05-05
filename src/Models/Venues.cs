using System.Collections.Generic;
using Newtonsoft.Json;

namespace SetlistNet.Models
{
    [JsonObject]
    /// <summary>
    /// A Result consisting of a list of venues.
    /// </summary>
    public class Venues : ApiArrayResult<Venue>
    {
        /// <summary>
        /// Gets or sets the list of venues.
        /// </summary>
        [JsonProperty(PropertyName = "venue")]
        internal List<Venue> Items
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

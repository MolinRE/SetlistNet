using System.Collections.Generic;
using Newtonsoft.Json;

namespace SetlistNet.Models
{
    [JsonObject]
    /// <summary>
    /// A Result consisting of a list of artists.
    /// </summary>
    public class Artists : ApiArrayResult<Artist>
    {
        /// <summary>
        /// Gets or sets the list of artists.
        /// </summary>
        [JsonProperty(PropertyName = "artist")]
        internal List<Artist> Items
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

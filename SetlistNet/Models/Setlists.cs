using System.Collections.Generic;
using Newtonsoft.Json;

namespace SetlistNet.Models
{
    [JsonObject]
    /// <summary>
    /// This class represents a Result - a list of setlists.
    /// </summary>
    public class Setlists : ApiArrayResult<Setlist>
    {
        /// <summary>
        /// Gets or sets the list of setlists
        /// </summary>
        [JsonProperty(PropertyName = "setlist")]
        internal List<Setlist> Items
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

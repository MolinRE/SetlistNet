using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// A Result consisting of a list of venues.
    /// </summary>
    public class Venues : ApiArrayResult<Venue>
    {
        /// <summary>
        /// Gets or sets the list of venues.
        /// </summary>
        [JsonPropertyName("venue")]
        internal List<Venue> Items
        {
            get => _items;
            set => _items = value;
        }

        public override string ToString()
        {
            return $"Count = {Items.Count}";
        }
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// A Result consisting of a list of artists.
    /// </summary>
    public class Artists : ApiArrayResult<Artist>
    {
        /// <summary>
        /// Gets or sets the list of artists.
        /// </summary>
        [JsonPropertyName("artist")]
        internal List<Artist> Items
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

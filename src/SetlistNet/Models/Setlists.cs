using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a Result - a list of setlists.
    /// </summary>
    public class Setlists : ApiArrayResult<Setlist>
    {
        /// <summary>
        /// Gets or sets the list of setlists
        /// </summary>
        [JsonPropertyName("setlist")]
        internal List<Setlist> Items
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

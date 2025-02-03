using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// A Result consisting of a list of countries.
    /// </summary>
    public class Countries : ApiArrayResult<Country>
    {
        /// <summary>
        /// Gets or sets the list of countries.
        /// </summary>
        [JsonPropertyName("country")]
        internal List<Country> Items
        {
            get => _items;
            set => _items = value;
        }

        public override string ToString()
        {
            return $"Count = {Items?.Count ?? 0}";
        }
    }
}

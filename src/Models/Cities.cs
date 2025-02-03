using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// A Result consisting of a list of cities.
    /// </summary>
    public class Cities : ApiArrayResult<City>
    {
        /// <summary>
        /// Gets or sets the list of cities.
        /// </summary>
        [JsonPropertyName("cities")]
        internal List<City> Items
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

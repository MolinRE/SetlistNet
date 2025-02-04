using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    public class Tour
    {
        /// <summary>
        /// Gets or sets the city's name, depending on the language valid values are "München" or "Munich".
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        public Tour()
        {
        }

        public Tour(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}

using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    public class Tour
    {
        private string _name;

        /// <summary>
        /// Gets or sets the city's name, depending on the language valid values are e.g. "Mchen" or "Munich".
        /// </summary>
        [JsonPropertyName("name")]
        public string Name
        {
            get => this._name;
            set => this._name = value;
        }

        public Tour()
        {
        }

        public Tour(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return "Name = " + Name;
        }
    }
}

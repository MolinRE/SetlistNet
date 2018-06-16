using Newtonsoft.Json;

namespace SetlistNet.Models
{
    public class Tour
    {
        private string _name;

        /// <summary>
        /// Gets or sets the city's name, depending on the language valid values are e.g. "Mchen" or "Munich".
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
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

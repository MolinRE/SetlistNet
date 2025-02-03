using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a country on earth.
    /// </summary>
    public class Country
    {
        #region Private Fields
        private string _name;
        private string _code;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the country's name. Can be a localized name - e.g. "Austria" or "Österreich" for Austria if the German name was requested.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name
        {
            get => this._name;
            set => this._name = value;
        }
        /// <summary>
        /// Gets or sets the country's ISO code. E.g. "ie" for Ireland.
        /// <para>See: <see cref="http://www.iso.org/iso/english_country_names_and_code_elements"/></para>
        /// </summary>
        [JsonPropertyName("code")]
        public string Code
        {
            get => this._code;
            set => this._code = value;
        }
        #endregion

        public Country()
        {

        }

        public Country(string name) : this()
        {
            Name = name;
        }

        public Country(string name, string code)
            : this(name)
        {
            Code = code;
        }

        public override string ToString()
        {
            return string.Format("Name = " + Name);
        }
    }
}

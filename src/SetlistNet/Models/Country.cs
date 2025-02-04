using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a country on earth.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Gets or sets the country's name. Can be a localized name - e.g. "Austria" or "Österreich" for Austria if the German name was requested.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the country's ISO code. E.g. "ie" for Ireland.
        /// <para>See: <see cref="http://www.iso.org/iso/english_country_names_and_code_elements"/></para>
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        public Country()
        {

        }

        public Country(string name, string? code = null)
        {
            Name = name;
            Code = code;
        }

        public override string ToString() => Name;
    }
}

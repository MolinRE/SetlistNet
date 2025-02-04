using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a city where Venues are located. 
    /// Most of the original city data was taken from <a href="http://geonames.org">Geonames.org</a>.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Gets or sets unique identifier
        /// <para>
        /// This is <c>geoNameId</c> property in <a href="http://geonames.org">Geonames.org</a>.
        /// Setlist.fm API returns <c>cu:aa1a96e1-06c7-11e6-b736-22000bb3106b</c> for <c>id=3d59d97</c>, so switching to int temporarily
        /// </para>
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the city's name, depending on the language valid values are e.g. "Müchen" or "Munich"
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of city's state
        /// </summary>
        /// <example>Bavaria or Florida</example>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the code of the city's state. For most countries this two-digit numeric code, 
        /// with which the state can be identified uniquely in the specific Country. See remarks for more info.
        /// <para>E.g. "CA" or 02</para>
        /// </summary>
        /// <remarks>
        /// Valid examples are "CA" or "02" which in turn get uniquely identifiable 
        /// when combined with the state's country:
        /// 
        /// "US.CA" for California, United States or
        /// "DE.02" for Bavaria, Germany
        /// 
        /// For a complete list of available states (that aren't necessarily used in this database) 
        /// is available in <see cref="http://download.geonames.org/export/dump/admin1Codes.txt"/> a textfile on geonames.org.
        /// 
        /// Note that this code is only unique combined with the city's Country. The code alone is not unique.
        /// </remarks>
        [JsonPropertyName("stateCode")]
        public string StateCode { get; set; }

        /// <summary>
        /// Gets or sets the city's coordinates. Usually the coordinates of the city centre are used
        /// </summary>
        [JsonPropertyName("coords")]
        public Coords Coords { get; set; }

        /// <summary>
        /// Gets or sets the city's country
        /// </summary>
        [JsonPropertyName("country")]
        public Country Country { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(State) ? $"{Name} ({Country.Name})" : $"{Name}, {State} ({Country.Name})";
        }
    }
}

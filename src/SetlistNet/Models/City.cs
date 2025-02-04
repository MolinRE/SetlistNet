using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// This class represents a city where Venues are located. 
/// Most of the original city data was taken from <a href="http://geonames.org">Geonames.org</a>.
/// <seealso cref="http://geonames.org"/>
/// </summary>
public class City(string id, string name, Coords coords, Country country, string? state, string? stateCode)
{
    /// <summary>
    /// Gets or sets unique identifier
    /// <para>
    /// This is <c>geoNameId</c> property in <a href="http://geonames.org">Geonames.org</a>.
    /// Setlist.fm API returns <c>cu:aa1a96e1-06c7-11e6-b736-22000bb3106b</c> for <c>id=3d59d97</c>, so switching to int temporarily
    /// </para>
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = id;

    /// <summary>
    /// Gets or sets the city's name, depending on the language valid values are e.g. "Müchen" or "Munich"
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the name of city's state
    /// </summary>
    /// <example>Bavaria or Florida</example>
    [JsonPropertyName("state")]
    public string? State { get; set; } = state;

    /// <summary>
    /// The code of the city's state. For most countries this is a two-digit numeric code, with which the state can
    /// be identified uniquely in the specific <see cref="Country"/>. The code can also be a string for other cities.
    /// Valid examples are <c>"CA"</c> or <c>"02"</c> which in turn get uniquely identifiable when combined with the state's
    /// country: "US.CA" for California, United States or "DE.02" for Bavaria, Germany.
    /// <para>
    /// For a complete list of available states (that aren't necessarily used in this database) is available in <a href="http://download.geonames.org/export/dump/admin1CodesASCII.txt">a textfile on geonames.org</a>.
    /// Note that this code is only unique combined with the city's Country. The code alone is not unique.
    /// </para>
    /// </summary>
    /// <example>"US.CA" for California, United States or "DE.02" for Bavaria, Germany</example>
    [JsonPropertyName("stateCode")]
    public string? StateCode { get; set; } = stateCode;

    /// <summary>
    /// Gets or sets the city's coordinates. Usually the coordinates of the city centre are used
    /// </summary>
    [JsonPropertyName("coords")]
    public Coords Coords { get; set; } = coords;

    /// <summary>
    /// Gets or sets the city's country
    /// </summary>
    [JsonPropertyName("country")]
    public Country Country { get; set; } = country;

    public override string ToString()
    {
        return string.IsNullOrEmpty(State) ? $"{Name} ({Country.Name})" : $"{Name}, {State} ({Country.Name})";
    }
}
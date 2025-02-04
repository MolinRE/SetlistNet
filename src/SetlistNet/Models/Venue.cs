using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// This class represents a venue. It's usually the name of the venue and city combined. 
/// <para>See remarks for more info</para>
/// </summary>
/// <remarks> 
/// Venues are places where concerts take place. 
/// They usually consist of a venue name and a city - but there are also some venues that do not have a city attached yet. 
/// In such a case, the city simply isn't set and the city and country may (but do not have to) be in the name.
/// </remarks>
public class Venue(string id, string name, City city, string url)
{
    /// <summary>
    /// Gets or sets unique identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = id;

    /// <summary>
    /// Gets or sets the name of the venue, usually without city and country. 
    /// <para>E.g. "Madison Square Garden" or "Royal Albert Hall"</para>
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the city in which the venue is located.
    /// </summary>
    [JsonPropertyName("city")]
    public City City { get; set; } = city;

    /// <summary>
    /// Gets or sets the attribution url.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = url;

    public override string ToString() => Name;
}
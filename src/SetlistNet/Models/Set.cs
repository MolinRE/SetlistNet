using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// A setlist consists of different (at least one) sets.
/// Sets can either be sets as defined in the <a href="http://www.setlist.fm/guidelines">Guidelines</a> or encores.
/// <seealso cref="!:http://www.setlist.fm/guidelines"/>
/// </summary>
public class Set(IReadOnlyList<Song> songs, string? name, int? encore)
{
    /// <summary>
    /// If the set is an encore, this property gets or sets the number of the encore,
    /// starting with 1 for the first encore, 2 for the second and so on
    /// </summary>
    [JsonPropertyName("encore")]
    public int? Encore { get; set; } = encore;

    /// <summary>
    /// Gets or sets the description/name of the set
    /// </summary>
    /// <example>"Acoustic set" or "Paul McCartney solo"</example>
    [JsonPropertyName("name")]
    public string? Name { get; set; } = name;

    /// <summary>
    /// Gets or sets this set's songs
    /// </summary>
    [JsonPropertyName("song")]
    public IReadOnlyList<Song> Songs { get; set; } = songs;

    public override string ToString()
    {
        return Encore.HasValue ? $"Encore {Encore}" : Name ?? $"Songs = {Songs.Count}";
    }
}
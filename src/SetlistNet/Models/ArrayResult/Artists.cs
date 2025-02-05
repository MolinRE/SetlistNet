using SetlistNet.Models.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models.ArrayResult;

/// <summary>
/// A Result consisting of a list of artists.
/// </summary>
public class Artists(IReadOnlyList<Artist> artist) : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of artists.
    /// </summary>
    [JsonPropertyName("artist")]
    public IReadOnlyList<Artist> Artist { get; set; } = artist;

    public override string ToString()
    {
        return $"Count = {Artist.Count}";
    }
}
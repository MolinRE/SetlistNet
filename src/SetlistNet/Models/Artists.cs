using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// A Result consisting of a list of artists.
/// </summary>
public class Artists : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of artists.
    /// </summary>
    [JsonPropertyName("artist")]
    public IReadOnlyCollection<Artist> Items { get; set; }

    public override string ToString()
    {
        return $"Count = {Items.Count}";
    }
}
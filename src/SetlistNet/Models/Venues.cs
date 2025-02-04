using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// A Result consisting of a list of venues.
/// </summary>
public class Venues : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of venues.
    /// </summary>
    [JsonPropertyName("venue")]
    public IReadOnlyCollection<Venue> Items { get; set; }

    public override string ToString()
    {
        return $"Count = {Items.Count}";
    }
}
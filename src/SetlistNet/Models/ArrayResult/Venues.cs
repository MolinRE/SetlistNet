using SetlistNet.Models.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models.ArrayResult;

/// <summary>
/// A Result consisting of a list of venues.
/// </summary>
public class Venues(IReadOnlyList<Venue> venue) : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of venues.
    /// </summary>
    [JsonPropertyName("venue")]
    public IReadOnlyList<Venue> Venue { get; set; } = venue;

    public override string ToString()
    {
        return $"Count = {Venue.Count}";
    }
}
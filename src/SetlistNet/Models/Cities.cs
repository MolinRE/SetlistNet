using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// A Result consisting of a list of cities.
/// </summary>
public class Cities : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of cities.
    /// </summary>
    [JsonPropertyName("cities")]
    public IReadOnlyCollection<City> Items { get; set; }

    public override string ToString()
    {
        return $"Count = {Items.Count}";
    }
}
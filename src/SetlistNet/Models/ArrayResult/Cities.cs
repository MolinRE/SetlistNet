using SetlistNet.Models.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models.ArrayResult;

/// <summary>
/// A Result consisting of a list of cities.
/// </summary>
public class Cities(IReadOnlyList<City> items) : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of cities.
    /// </summary>
    [JsonPropertyName("cities")]
    public IReadOnlyList<City> Items { get; set; } = items;

    public override string ToString() => $"Count = {Items.Count}";
}
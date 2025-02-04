using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// A Result consisting of a list of countries.
/// </summary>
public class Countries : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of countries.
    /// </summary>
    [JsonPropertyName("country")]
    public IReadOnlyCollection<Country> Items { get; set; }

    public override string ToString()
    {
        return $"Count = {Items?.Count ?? 0}";
    }
}
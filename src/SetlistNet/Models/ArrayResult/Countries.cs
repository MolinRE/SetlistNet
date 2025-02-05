using SetlistNet.Models.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models.ArrayResult;

/// <summary>
/// A Result consisting of a list of countries.
/// </summary>
public class Countries(IReadOnlyList<Country> country) : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of countries.
    /// </summary>
    [JsonPropertyName("country")]
    public IReadOnlyList<Country> Country { get; set; } = country;

    public override string ToString()
    {
        return $"Count = {Country?.Count ?? 0}";
    }
}
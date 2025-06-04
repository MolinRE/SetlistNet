using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models;

public class Sets(IReadOnlyList<Set> set)
{
    /// <summary>
    /// Gets or sets the collection of sets
    /// </summary>
    [JsonPropertyName("set")]
    public IReadOnlyList<Set> Set { get; set; } = set;

    public override string ToString() => $"Count = {Set.Count}";
}
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models;

public class Sets
{
    /// <summary>
    /// Gets or sets the collection of sets
    /// </summary>
    [JsonPropertyName("set")]
    public IReadOnlyCollection<Set> SetCollection { get; set; }

    public override string ToString()
    {
        return $"Count = {SetCollection.Count}";
    }
}
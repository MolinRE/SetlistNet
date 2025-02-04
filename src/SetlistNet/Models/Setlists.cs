using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// This class represents a Result - a list of setlists.
/// </summary>
public class Setlists : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of setlists
    /// </summary>
    [JsonPropertyName("setlist")]
    public IReadOnlyCollection<Setlist> Items { get; set; }

    public override string ToString()
    {
        return $"Count = {Items.Count}";
    }
}
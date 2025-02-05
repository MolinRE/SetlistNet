using SetlistNet.Models.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models.ArrayResult;

/// <summary>
/// This class represents a Result - a list of setlists.
/// </summary>
public class Setlists(IReadOnlyList<Setlist> setlist) : ApiArrayResult
{
    /// <summary>
    /// Gets or sets the list of setlists
    /// </summary>
    [JsonPropertyName("setlist")]
    public IReadOnlyList<Setlist> Setlist { get; set; } = setlist;

    public override string ToString()
    {
        return $"Count = {Setlist.Count}";
    }
}
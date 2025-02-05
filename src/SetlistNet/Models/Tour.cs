using System.Text.Json.Serialization;

namespace SetlistNet.Models;

public class Tour(string name)
{
    /// <summary>
    /// Gets or sets the city's name, depending on the language valid values are "München" or "Munich"
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    public override string ToString()
    {
        return Name;
    }
}
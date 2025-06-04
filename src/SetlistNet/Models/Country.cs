using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// This class represents a country on earth.
/// </summary>
[method: JsonConstructor]
public class Country(string name, string code)
{
    /// <summary>
    /// Gets or sets the country's name. Can be a localized name - e.g. "Austria" or "Österreich" for Austria if the German name was requested.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the country's <a href="http://www.iso.org/iso/english_country_names_and_code_elements">ISO code</a>
    /// <seealso cref="!:http://www.iso.org/iso/english_country_names_and_code_elements"/>
    /// </summary>
    /// <example><c>ie</c> for Ireland</example>
    [JsonPropertyName("code")]
    public string Code { get; set; } = code;

    public override string ToString() => Name;
}
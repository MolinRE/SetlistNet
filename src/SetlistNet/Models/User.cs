using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// This class represents a user
/// </summary>
public class User(string userId, string url)
{
    [JsonPropertyName("userId")]
    public string UserId { get; set; } = userId;

    [JsonPropertyName("url")]
    public string Url { get; set; } = url;

    public override string ToString()
    {
        return UserId;
    }
}
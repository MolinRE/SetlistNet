using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// This class represents coordinates of a point on the globe. Mostly used for <see cref="City"/>
/// </summary>
public class Coords(double longitude, double latitude)
{
    /// <summary>
    /// Gets or sets the longitude part of the coordinates
    /// </summary>
    [JsonPropertyName("long")]
    public double Longitude { get; set; } = longitude;

    /// <summary>
    /// Gets or sets the latitude part of the coordinates
    /// </summary>
    [JsonPropertyName("lat")]
    public double Latitude { get; set; } = latitude;

    /// <summary>
    /// Returns latitude and longitude in the format <c>lat,long</c>
    /// </summary>
    /// <returns>String representing latitude and longitude separated by comma</returns>
    public override string ToString() => $"{Latitude}, {Longitude}";
}
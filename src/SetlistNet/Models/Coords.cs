using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents coordinates of a point on the globe. Mostly used for <paramref name="Cities"/>.
    /// </summary>
    public class Coords
    {
        /// <summary>
        /// Gets or sets the longitude part of the coordinates.
        /// </summary>
        [JsonPropertyName("long")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude part of the coordinates.
        /// </summary>
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Returns latitude and longitude in the format "lat,long".
        /// </summary>
        /// <returns>String representing latitude and longitude separated by comma</returns>
        public override string ToString() => $"{Latitude}, {Longitude}";
    }
}

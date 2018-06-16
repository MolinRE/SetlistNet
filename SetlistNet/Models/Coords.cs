using Newtonsoft.Json;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents coordinates of a point on the globe. Mostly used for <paramref name="Cities"/>.
    /// </summary>
    public class Coords
    {
        #region Private Fields
        private double? _longitude;
        private bool _longitudeSpecified;
        private double? _latitude;
        private bool _latitudeSpecified;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the longitude part of the coordinates.
        /// </summary>
        [JsonProperty(PropertyName = "long")]
        public double Longitude
        {
            get
            {
                return this._longitude.GetValueOrDefault();
            }
            set
            {
                this._longitude = value;
                this._longitudeSpecified = true;
            }
        }
        /// <summary>
        /// Gets or sets whether the "Longitude" property should be included in the output.
        /// </summary>
        public bool LongitudeSpecified
        {
            get
            {
                return this._longitudeSpecified;
            }
            set
            {
                this._longitudeSpecified = value;
            }
        }
        /// <summary>
        /// Gets or sets the latitude part of the coordinates.
        /// </summary>
        [JsonProperty(PropertyName = "lat")]
        public double Latitude
        {
            get
            {
                return this._latitude.GetValueOrDefault();
            }
            set
            {
                this._latitude = value;
                this._latitudeSpecified = true;
            }
        }
        /// <summary>
        /// Gets or sets whether the "Latitude" property should be included in the output.
        /// </summary>
        public bool LatitudeSpecified
        {
            get
            {
                return this._latitudeSpecified;
            }
            set
            {
                this._latitudeSpecified = value;
            }
        }
        #endregion

        /// <summary>
        /// Returns latitude and longitude in the format "lat,long".
        /// </summary>
        /// <returns>String representing latitude and longitude separated by comma.</returns>
        public override string ToString()
        {
            return string.Format("Latitude = {0}, Longitude = {1}", Latitude, Longitude);
        }
    }
}

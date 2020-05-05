using Newtonsoft.Json;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a song that is part of a <paramref name="Set"/>.
    /// </summary>
    public class Song
    {
        #region Private Fields
        private string _name;
        private Artist _with;
        private Artist _cover;
        private string _info;
        #endregion

        #region Properties
        /// <summary>
        /// The name of the song. E.g. "Yesterday" or "Wish You Were Here".
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        /// <summary>
        /// A different <paramref name="Artist"/> than the performing one that joined the stage for this song.
        /// </summary>
        [JsonProperty(PropertyName = "with")]
        public Artist With
        {
            get
            {
                return this._with;
            }
            set
            {
                this._with = value;
            }
        }
        /// <summary>
        /// The original <paramref name="Artist"/> of this song, if different to the performing artist.
        /// </summary>
        [JsonProperty(PropertyName = "cover")]
        public Artist Cover
        {
            get
            {
                return this._cover;
            }
            set
            {
                this._cover = value;
            }
        }
        /// <summary>
        /// Special incidents or additional information about the way the song was performed at this specific concert. See the Guidelines complete list of allowed content.
        /// <para>Guidelines: <see cref="http://www.setlist.fm/guidelines"/>.</para>
        /// </summary>
        [JsonProperty(PropertyName = "info")]
        public string Info
        {
            get
            {
                return this._info;
            }
            set
            {
                this._info = value;
            }
        }
        #endregion

        /// <summary>
        /// Returns the <paramref name="Name"/> propertu of the object.
        /// </summary>
        /// <returns>A string that represents <paramref name="Name"/> property.</returns>
        public override string ToString()
        {
            return "Name = " + Name;
        }
    }
}

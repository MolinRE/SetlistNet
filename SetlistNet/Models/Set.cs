using System.Collections.Generic;
using Newtonsoft.Json;

namespace SetlistNet.Models
{
    /// <summary>
    /// A setlist consists of different (at least one) sets. Sets can either be sets as defined in the Guidelines or encores.
    /// <para>See: <see cref="http://www.setlist.fm/guidelines"/>.</para>
    /// </summary>
    public class Set
    {
        #region Private Fields
        private int? _encore;
        private bool _encoreSpecified;
        private string _name;
        private List<Song> _songs;
        #endregion

        #region Properties
        /// <summary>
        /// If the set is an encore, this property gets or sets the number of the encore, starting with 1 for the first encore, 2 for the second and so on.
        /// </summary>
        [JsonProperty(PropertyName = "encore")]
        public int Encore
        {
            get
            {
                return this._encore.GetValueOrDefault();
            }
            set
            {
                this._encore = value;
                this._encoreSpecified = true;
            }
        }
        /// <summary>
        /// Gets or sets whether the "Encore" property should be included in the output.
        /// </summary>
        public bool EncoreSpecified
        {
            get
            {
                return this._encoreSpecified;
            }
            set
            {
                this._encoreSpecified = value;
            }
        }
        /// <summary>
        /// Gets or sets the description/name of the set. E.g. "Acoustic set" or "Paul McCartney solo".
        /// </summary>
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
        /// Gets or sets this set's songs.
        /// </summary>
        [JsonProperty(PropertyName = "song")]
        public List<Song> Songs
        {
            get
            {
                return this._songs;
            }
            set
            {
                this._songs = value;
            }
        }
        #endregion

        public override string ToString()
        {
            string encore = "";
            if (EncoreSpecified)
                encore = "[Encore " + Encore + "] ";
            return string.Format("{0}Songs = {1}", encore, Songs.Count);
        }
    }
}

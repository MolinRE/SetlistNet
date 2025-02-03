using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a user.
    /// </summary>
    public class User
    {
        #region Private Fields
        private string _flickr;
        private string _twitter;
        private string _website;
        private string _userId;
        private string _lastFm;
        private string _mySpace;
        private string _fullname;
        private string _about;
        private string _url;
        #endregion

        #region Properties
        [JsonPropertyName("flickr")]
        public string Flickr
        {
            get => this._flickr;
            set => this._flickr = value;
        }

        [JsonPropertyName("twitter")]
        public string Twitter
        {
            get => this._twitter;
            set => this._twitter = value;
        }

        [JsonPropertyName("website")]
        public string Website
        {
            get => this._website;
            set => this._website = value;
        }

        [JsonPropertyName("userId")]
        public string UserId
        {
            get => this._userId;
            set => this._userId = value;
        }

        [JsonPropertyName("lastFm")]
        public string LastFm
        {
            get => this._lastFm;
            set => this._lastFm = value;
        }

        [JsonPropertyName("mySpace")]
        public string MySpace
        {
            get => this._mySpace;
            set => this._mySpace = value;
        }

        [JsonPropertyName("fullname")]
        public string Fullname
        {
            get => this._fullname;
            set => this._fullname = value;
        }

        [JsonPropertyName("about")]
        public string About
        {
            get => this._about;
            set => this._about = value;
        }

        [JsonPropertyName("url")]
        public string Url
        {
            get => this._url;
            set => this._url = value;
        }
        #endregion

        public override string ToString()
        {
            return "UserId = " + UserId;
        }
    }
}

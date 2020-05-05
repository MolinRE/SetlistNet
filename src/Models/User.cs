using Newtonsoft.Json;

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
        [JsonProperty(PropertyName = "flickr")]
        public string Flickr
        {
            get
            {
                return this._flickr;
            }
            set
            {
                this._flickr = value;
            }
        }

        [JsonProperty(PropertyName = "twitter")]
        public string Twitter
        {
            get
            {
                return this._twitter;
            }
            set
            {
                this._twitter = value;
            }
        }

        [JsonProperty(PropertyName = "website")]
        public string Website
        {
            get
            {
                return this._website;
            }
            set
            {
                this._website = value;
            }
        }

        [JsonProperty(PropertyName = "userId")]
        public string UserId
        {
            get
            {
                return this._userId;
            }
            set
            {
                this._userId = value;
            }
        }

        [JsonProperty(PropertyName = "lastFm")]
        public string LastFm
        {
            get
            {
                return this._lastFm;
            }
            set
            {
                this._lastFm = value;
            }
        }

        [JsonProperty(PropertyName = "mySpace")]
        public string MySpace
        {
            get
            {
                return this._mySpace;
            }
            set
            {
                this._mySpace = value;
            }
        }

        [JsonProperty(PropertyName = "fullname")]
        public string Fullname
        {
            get
            {
                return this._fullname;
            }
            set
            {
                this._fullname = value;
            }
        }

        [JsonProperty(PropertyName = "about")]
        public string About
        {
            get
            {
                return this._about;
            }
            set
            {
                this._about = value;
            }
        }

        [JsonProperty(PropertyName = "url")]
        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }
        #endregion

        public override string ToString()
        {
            return "UserId = " + UserId;
        }
    }
}

using Newtonsoft.Json;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents an artist. An artist is a musician or a group of musicians. 
    /// Each artist has a definite Musicbrainz Identifier (MBID) with which the artist can be uniquely identified.
    /// <para>See <seealso cref="http://wiki.musicbrainz.org/MBID"/> for more info about Musicbrainz ID.</para>
    /// </summary>
    public class Artist 
    {
        #region Private Fields
        private string _disambiguation;
        private string _mbid;
        private int? _tmid;
        private bool _tmidSpecified;
        private string _name;
        private string _sortName;
        private string _url;
        #endregion

        #region Properties
        /// <summary>
        /// Disambiguation to distinguish between artists with same names.
        /// </summary>
        [JsonProperty(PropertyName = "disambiguation")]
        public string Disambiguation
        {
            get
            {
                return this._disambiguation;
            }
            set
            {
                this._disambiguation = value;
            }
        }
        /// <summary>
        /// Gets or sets unique Musicbrainz Identifier (MBID), e.g. "b10bbbfc-cf9e-42e0-be17-e2c3e1d2600d" (The Beatles).
        /// </summary>
        [JsonProperty(PropertyName = "mbid")]
        public string MBID
        {
            get
            {
                return this._mbid;
            }
            set
            {
                this._mbid = value;
            }
        }
        /// <summary>
        /// Gets or sets unique Ticket Master Identifier (TMID), e.g. 1953.
        /// </summary>
        [JsonProperty(PropertyName = "tmid")]
        public int TMID
        {
            get
            {
                return this._tmid.GetValueOrDefault();
            }
            set
            {
                this._tmid = value;
                this._tmidSpecified = true;
            }
        }
        /// <summary>
        /// Gets or sets the <paramref name="TMID"/> property should be included in the output.
        /// </summary>
        public bool TMIDSpecified
        {
            get
            {
                return this._tmidSpecified;
            }
            set
            {
                this._tmidSpecified = value;
            }
        }
        /// <summary>
        /// Gets or sets the artist's name, e.g. "The Beatles" or "Bruce Springsteen".
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
        /// Gets or sets the artist's sort name, e.g. "Beatles, The" or "Springsteen, Bruce".
        /// </summary>
        [JsonProperty(PropertyName = "sortName")]
        public string SortName
        {
            get
            {
                return this._sortName;
            }
            set
            {
                this._sortName = value;
            }
        }
        /// <summary>
        /// Gets or sets the url to artist's setlists' page on Setlist.fm.
        /// </summary>
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

        /// <summary>
        /// Gets the url to artist's stats' page on Setlist.fm.
        /// </summary>
        public string UrlStats
        {
            get
            {
                return Url.Replace("/setlists/", "/stats/");
            }
        }

        public string NameWithDisambiguation
        {
            get
            {
                if (string.IsNullOrEmpty(Disambiguation))
                    return Name;
                else
                    return Name + " (" + Disambiguation + ")";
            }
        }
        #endregion

        public Artist()
        {
        }

        public Artist(string name)
            : this()
        {
            Name = name;
        }

        public override string ToString()
        {
            return "Name = " + Name;
        }
    }
}

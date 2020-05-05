using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a setlist. 
    /// <para>
    /// Please remember that setlist.fm is a wiki, so there are different versions of the same setlist. 
    /// Thus, the best way to check whether two setlists are the same is to use <paramref name="VersioId"/>.
    /// </para>
    /// </summary>
    /// <remarks>
    /// A setlist can be distinguished from other setlists by its unique id. But as setlist.fm works the wiki way, there can
    /// be different versions of one setlist (each time a user updates a setlist a new version gets created). 
    /// These different versions have a unique id on its own. So setlists can have the same id although they differ as far 
    /// as the content is concerned - thus the best way to check if two setlists are the same is to compare their versionIds.
    /// </remarks>
    [JsonObject]
    public class Setlist
    {
        #region Private Fields
        private Artist _artist;
        private Venue _venue;
        private Tour _tour;
        private List<Set> _sets;
        private string _info;
        private string _url;
        private string _id;
        private string _versionId;
        private string _eventDate;
        private string _lastUpdated;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the setlist's artist.
        /// </summary>
        [JsonProperty(PropertyName = "artist")]
        public Artist Artist
        {
            get
            {
                return this._artist;
            }
            set
            {
                this._artist = value;
            }
        }

        /// <summary>
        /// Gets or sets the setlist's venue.
        /// </summary>
        [JsonProperty(PropertyName = "venue")]
        public Venue Venue
        {
            get
            {
                return this._venue;
            }
            set
            {
                this._venue = value;
            }
        }

        /// <summary>
        /// Gets or sets the tour in which the band performed setlist.
        /// </summary>
        [JsonProperty(PropertyName = "tour")]
        public Tour Tour
        {
            get
            {
                return this._tour;
            }
            set
            {
                this._tour = value;
            }
        }

        [JsonIgnore]
        public string TourName
        {
            get
            {
                if (Tour == null)
                    return null;
                return Tour.Name;
            }
            set
            {
                if (Tour == null)
                    Tour = new Tour();
                Tour.Name = value;
            }
        }

        /// <summary>
        /// Gets or sets all sets of this setlist.
        /// </summary>
        [JsonIgnore]
        public List<Set> Sets
        {
            get
            {
                return this._sets;
            }
            set
            {
                this._sets = value;
            }
        }

        /// <summary>
        /// Gets or sets additional information on the concert - see the Guidelines for a complete list of allowed content.
        /// <para>See: <see cref="http://www.setlist.fm/guidelines"/>.</para>
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

        /// <summary>
        /// Gets or sets the attribution url to which you have to link to wherever you use data from this setlist in your application.
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
        /// Gets or sets unique identifier of setlist.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        /// <summary>
        /// Gets or sets unique identifier of the version of setlist.
        /// </summary>
        [JsonProperty(PropertyName = "versionId")]
        public string VersionId
        {
            get
            {
                return this._versionId;
            }
            set
            {
                this._versionId = value;
            }
        }

        /// <summary>
        /// Gets or sets date of the concert in the format "dd-MM-yyyy", e.g. 31-03-2007.
        /// </summary>
        [JsonProperty(PropertyName = "eventDate")]
        public string EventDate
        {
            get
            {
                return this._eventDate;
            }
            set
            {
                this._eventDate = value;
            }
        }

        /// <summary>
        /// Gets or sets date, time and time zone of the last update to this setlist in the format "yyyy-MM-dd'T'HH:mm:ss.SSSZZZZZ".
        /// </summary>
        [JsonProperty(PropertyName = "lastUpdated")]
        public string LastUpdated
        {
            get
            {
                return this._lastUpdated;
            }
            set
            {
                this._lastUpdated = value;
            }
        }
        #endregion

        [JsonExtensionData]
        private IDictionary<string, JToken> _additionalData;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Sets = _additionalData["sets"].First.First.ToObject<List<Set>>();
        }

        public Setlist()
        {

        }

        public Setlist(Artist artist)
            : this()
        {
            Artist = artist;
        }

        public DateTime? GetEventDateTime()
        {
            DateTime result;
            if (DateTime.TryParseExact(EventDate, "dd-MM-yyyy", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            
            return null;
        }

        public string GetEventDateTime(string format)
        {
            var dt = GetEventDateTime();
            return dt.HasValue ? dt.Value.ToString(format) : "";
        }

        public string GetEventDateTime(string format, IFormatProvider provider)
        {
            var dt = GetEventDateTime();
            return dt.HasValue ? dt.Value.ToString(format, provider) : "";
        }

        public void SetEventDateTime(DateTime dt)
        {
            EventDate = dt.ToString("dd-MM-yyyy");
        }

        public ushort GetYear()
        {
            if (EventDate != null && EventDate.Length == 10)
            {
                return Convert.ToUInt16(EventDate.Substring(6));
            }
            else
                return 0;
        }

        public override string ToString()
        {
            return $"[{EventDate}] {Artist.Name} @ {Venue.Name}";
        }
    }
}

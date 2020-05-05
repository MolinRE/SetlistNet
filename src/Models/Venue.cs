using Newtonsoft.Json;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a venue. It's usually the name of the venue and city combined. 
    /// <para>See remarks for more info.</para>
    /// </summary>
    /// <remarks> 
    /// Venues are places where concerts take place. 
    /// They usually consist of a venue name and a city - but there are also some venues that do not have a city attached yet. 
    /// In such a case, the city simply isn't set and the city and country may (but do not have to) be in the name.
    /// </remarks>
    public class Venue
    {
        #region Private Fields
        private string _id;
        private string _name;
        private City _city;
        private string _url;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets unique identifier.
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
        /// Gets or sets the name of the venue, usually without city and country. 
        /// <para>E.g. "Madison Square Garden" or "Royal Albert Hall".</para>
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
        /// Gets or sets the city in which the venue is located.
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public City City
        {
            get
            {
                return this._city;
            }
            set
            {
                this._city = value;
            }
        }
        /// <summary>
        /// Gets or sets the attribution url.
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
        #endregion

        public Venue()
        {

        }

        public Venue(string name) : this()
        {
            Name = name;
        }

        public Venue(City city) : this()
        {
            City = city;
        }

        public override string ToString()
        {
            return "Name = " + Name;
        }
    }
}

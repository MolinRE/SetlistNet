using Newtonsoft.Json;
using SetlistNet.Models;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace SetlistNet
{
    public class SetlistApi
    {
        private readonly string language;
        private readonly string token;
        private static string root = "https://api.setlist.fm";
        private static string version = "1.0";

        public SetlistApi(string apiToken)
        {
            language = "en";
            token = apiToken;
        }

        public SetlistApi(string apiToken, string desiredLanguage)
            : this(apiToken)
        {
            language = desiredLanguage;
        }

        public Artist Artist(string mbid)
        {
            string url = string.Format("/artist/{0}", mbid);
            Artist artist = Load<Artist>(url);

            return artist;
        }

        public City City(int geoId)
        {
            string url = string.Format("/city/{0}", geoId);
            City artist = Load<City>(url);

            return artist;
        }

        /// <summary>
        /// Search for artists.
        /// </summary>
        /// <param name="searchFields">
        /// You must provide a value for at least one of the following properties:
        /// <para>MBID, TMID (set <code>TMIDSpecified = true</code>), Name.</para>
        /// </param>
        /// <param name="page">Page number to fetch.</param>
        /// <returns>A list of matching artist.</returns>
        public Artists SearchArtists(Artist searchFields, int page = 1)
        {
            StringBuilder query = new StringBuilder();
            if (searchFields != null)
            {
                if (!string.IsNullOrEmpty(searchFields.MBID))
                    query.AppendFormat("artistMbid={0}&", searchFields.MBID);
                if (searchFields.TMIDSpecified)
                    query.AppendFormat("artistTmid={0}&", searchFields.TMID);
                if (!string.IsNullOrEmpty(searchFields.Name))
                    query.AppendFormat("artistName={0}&", searchFields.Name);
            }

            string url = string.Format("/search/artists?{0}sort=relevance&p={1}", query.ToString(), page.ToString());
            Artists artists = Load<Artists>(url);

            return artists;
        }

        public Artists SearchArtists(string artistName, int page = 1)
        {
            return SearchArtists(new Artist(artistName), page);
        }

        /// <summary>
        /// Search for a city.
        /// </summary>
        /// <param name="searchFields">
        /// You must provide a value for at least one of the following properties:
        /// <para>Name, State, StateCode, Country.Code.</para>
        /// </param>
        /// <param name="page">Page number to fetch.</param>
        /// <returns>A list of matching cities.</returns>
        public Cities SearchCities(City searchFields, int page = 1)
        {
            StringBuilder query = new StringBuilder();
            if (searchFields != null)
            {
                if (!string.IsNullOrEmpty(searchFields.Name))
                    query.AppendFormat("name={0}&", searchFields.Name);
                if (!string.IsNullOrEmpty(searchFields.State))
                    query.AppendFormat("state={0}&", searchFields.State);
                if (!string.IsNullOrEmpty(searchFields.StateCode))
                    query.AppendFormat("stateCode={0}&", searchFields.StateCode);

                if (searchFields.Country != null)
                {
                    if (!string.IsNullOrEmpty(searchFields.Country.Code))
                        query.AppendFormat("country={0}&", searchFields.Country.Code);
                }

            }
            string url = string.Format("/search/cities?{0}p={1}", query, page);
            Cities cities = Load<Cities>(url);

            return cities;
        }

        public Countries SearchCountries()
        {
            string url = string.Format("/search/countries");
            Countries countries = Load<Countries>(url);

            return countries;
        }

        /// <summary>
        /// Search for setlists.
        /// </summary>
        /// <param name="searchFields">
        /// You must provide a value for at least one of the following properties:
        /// <para>Tour, LastUpdated, EventDate (you could set only a year, e.g. <code>00-00-2007</code>),</para>
        /// <para>Artist.Name, Artist.MBID, Artist.TMID (set <code>TMIDSpecified = true</code>)</para>
        /// <para>Venue.Id, Venue.Name, Venue.City.Id, Venue.City.Name. Venue.City.State, Venue.City.State.Code, Venue.City.Country.Code</para>
        /// </param>
        /// <param name="page">Page number to fetch.</param>
        /// <returns>A list of matching setlists.</returns>
        public Setlists SearchSetlists(Setlist searchFields, int page = 1)
        {
            StringBuilder query = new StringBuilder();
            if (searchFields != null)
            {
                if (!string.IsNullOrEmpty(searchFields.TourName))
                    query.AppendFormat("tour={0}&", searchFields.Tour);
                if (!string.IsNullOrEmpty(searchFields.LastUpdated))
                    query.AppendFormat("lastUpdate={0}&", searchFields.LastUpdated);
                if (!string.IsNullOrEmpty(searchFields.EventDate))
                {
                    if (searchFields.GetEventDateTime() != null)
                        query.AppendFormat("date={0}&", searchFields.EventDate);
                    else
                        if (searchFields.GetYear() != 0)
                        query.AppendFormat("year={0}&", searchFields.GetYear());
                }

                if (searchFields.Artist != null)
                {
                    if (!string.IsNullOrEmpty(searchFields.Artist.MBID))
                        query.AppendFormat("artistMbid={0}&", searchFields.Artist.MBID);
                    if (searchFields.Artist.TMIDSpecified)
                        query.AppendFormat("artistTmid={0}&", searchFields.Artist.TMID);
                    if (!string.IsNullOrEmpty(searchFields.Artist.Name))
                        query.AppendFormat("artistName={0}&", searchFields.Artist.Name);
                }
                if (searchFields.Venue != null)
                {
                    if (!string.IsNullOrEmpty(searchFields.Venue.Id))
                        query.AppendFormat("venueId={0}&", searchFields.Venue.Id);
                    if (!string.IsNullOrEmpty(searchFields.Venue.Name))
                        query.AppendFormat("venueName={0}&", searchFields.Venue.Name);

                    if (searchFields.Venue.City != null)
                    {
                        if (searchFields.Venue.City.Id != "0" && searchFields.Venue.City.Id != "")
                            query.AppendFormat("cityId={0}&", searchFields.Venue.City.Id);
                        if (!string.IsNullOrEmpty(searchFields.Venue.City.Name))
                            query.AppendFormat("cityName={0}&", searchFields.Venue.City.Name);
                        if (!string.IsNullOrEmpty(searchFields.Venue.City.State))
                            query.AppendFormat("state={0}&", searchFields.Venue.City.State);
                        if (!string.IsNullOrEmpty(searchFields.Venue.City.StateCode))
                            query.AppendFormat("stateCode={0}&", searchFields.Venue.City.StateCode);

                        if (searchFields.Venue.City.Country != null)
                        {
                            if (!string.IsNullOrEmpty(searchFields.Venue.City.Country.Code))
                                query.AppendFormat("countryCode={0}&", searchFields.Venue.City.Country.Code);
                        }
                    }
                }
            }

            string url = string.Format("/search/setlists?{0}p={1}", query.ToString(), page.ToString());
            Setlists setlists = Load<Setlists>(url);

            return setlists;
        }

        /// <summary>
        /// Search for venues.
        /// </summary>
        /// <param name="searchFields">
        /// You must provide a value for at least one of the following properties:
        /// <para>Name, City.Id, City.Name, City.State, City.StateCode, City.Country.Code.</para>
        /// </param>
        /// <param name="page">Page number to fetch.</param>
        /// <returns>A list of matching venues.</returns>
        public Venues SearchVenues(Venue searchFields, int page = 1)
        {
            StringBuilder query = new StringBuilder();

            if (searchFields != null)
            {
                if (!string.IsNullOrEmpty(searchFields.Name))
                    query.AppendFormat("name={0}&", searchFields.Name);

                if (searchFields.City != null)
                {
                    if (searchFields.City.Id != "0" && searchFields.City.Id != "")
                        query.AppendFormat("cityId={0}&", searchFields.City.Id);
                    if (!string.IsNullOrEmpty(searchFields.City.Name))
                        query.AppendFormat("cityName={0}&", searchFields.City.Name);
                    if (!string.IsNullOrEmpty(searchFields.City.State))
                        query.AppendFormat("state={0}&", searchFields.City.State);
                    if (!string.IsNullOrEmpty(searchFields.City.StateCode))
                        query.AppendFormat("stateCode={0}&", searchFields.City.StateCode);

                    if (searchFields.City.Country != null)
                    {
                        if (!string.IsNullOrEmpty(searchFields.City.Country.Code))
                            query.AppendFormat("country={0}&", searchFields.City.Country.Code);
                    }
                }
            }

            string url = string.Format("/search/venues?{0}p={1}", query.ToString(), page.ToString());
            Venues venues = Load<Venues>(url);

            return venues;
        }

        /// <summary>
        /// Returns the current version of a setlist. E.g. if you pass the id of a setlist that got edited since you last accessed it, you'll get the current version.
        /// </summary>
        /// <param name="setlistId">The setlist id.</param>
        /// <param name="page">Page number to fetch.</param>
        /// <returns>The setlist for the provided id.</returns>
        public Setlist Setlist(string setlistId)
        {
            string url = string.Format("/setlist/{0}", setlistId);
            Setlist setlist = Load<Setlist>(url);

            return setlist;
        }

        public User User(string userId)
        {
            string url = string.Format("/user/{0}", userId);
            User user = Load<User>(url);

            return user;
        }

        public Venue Venue(string venueId)
        {
            string url = string.Format("/venue/{0}", venueId);
            Venue venue = Load<Venue>(url);

            return venue;
        }

        public Setlists ArtistSetlists(string mbid, int page = 1)
        {
            string url = string.Format("/artist/{0}/setlists?p={1}", mbid, page);
            Setlists artistSetlists = Load<Setlists>(url);

            return artistSetlists;
        }

        /// <summary>
        /// Returns a setlist for the given versionId. The setlist returned isn't necessarily the most recent version.
        /// E.g. if you pass the versionId of a setlist that got edited since you last accessed it, you'll get the same version as last time.
        /// </summary>
        /// <param name="versionId">The version ID.</param>
        /// <returns>The setlist for the provided versionId.</returns>
        public Setlist SetlistVersion(string versionId)
        {
            string url = string.Format("/setlist/version/{0}", versionId);
            Setlist setlist = Load<Setlist>(url);

            return setlist;
        }

        /// <summary>
        /// Get a list of setlists of concerts attended by a user.
        /// </summary>
        /// <param name="userId">A user's userId.</param>
        /// <param name="page">Page number to fetch.</param>
        /// <returns>A list of setlists.</returns>
        public Setlists UserAttended(string userId, int page = 1)
        {
            string url = string.Format("/user/{0}/attended?p={1}", userId, page);
            Setlists attended = Load<Setlists>(url);

            return attended;
        }

        /// <summary>
        /// Get a list of setlists of concerts edited by a user. 
        /// <para>The list contains the current version, not the version edited.</para>
        /// </summary>
        /// <param name="userId">The user's userId.</param>
        /// <param name="page">Page number to fetch.</param>
        /// <returns>A list of setlists.</returns>
        public Setlists UserEdited(string userId, int page = 1)
        {
            string url = string.Format("/user/{0}/edited?p={1}", userId, page);
            Setlists edited = Load<Setlists>(url);

            return edited;
        }

        public Setlists VenueSetlists(string venueId, int page = 1)
        {
            string url = string.Format("/venue/{0}/setlists?p={1}", venueId, page);
            Setlists venueSetlists = Load<Setlists>(url);

            return venueSetlists;
        }

        public T Load<T>(string url)
        {
            Uri uri = new Uri(root + "/rest/" + version + url);

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Accept = "application/json";
            request.Headers.Add("x-api-key:" + token);
            request.Headers.Add("Accept-Language:" + language);
            var response = request.GetResponse();
            string value = "";
            using (var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                value = sr.ReadToEnd();
            }

            var result = JsonConvert.DeserializeObject<T>(value);
            return result;
        }
    }
}

using SetlistNet.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SetlistNet
{
    public class SetlistApi
    {
        private const string Host = "https://api.setlist.fm";
        private const string ApiVersion = "1.0";
        
        private readonly HttpClient _httpClient;

        public SetlistApi(string apiToken, string desiredLanguage = "en", HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("x-api-key", apiToken);
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", desiredLanguage);
        }

        public Task<Artist> Artist(string mbid) => Load<Artist>($"/artist/{mbid}");

        public Task<City> City(int geoId) => Load<City>($"/city/{geoId}");

        /// <summary>
        /// Search for artists.
        /// </summary>
        /// <param name="searchFields">
        /// You must provide a value for at least one of the following properties:
        /// <para>MBID, TMID, Name</para>
        /// </param>
        /// <param name="page">Page number to fetch</param>
        /// <returns>A list of matching artist</returns>
        /// <remarks>
        /// If you specify <c>TMID</c> param, you must set:
        /// <code>TMIDSpecified = true</code>
        /// </remarks>
        public Task<Artists> SearchArtists(Artist searchFields, int page = 1)
        {
            var query = new StringBuilder();
            if (searchFields != null)
            {
                if (!string.IsNullOrEmpty(searchFields.MBID))
                {
                    query.AppendFormat("artistMbid={0}&", searchFields.MBID);
                }

                if (searchFields.TMID.HasValue)
                {
                    query.AppendFormat("artistTmid={0}&", searchFields.TMID);
                }

                if (!string.IsNullOrEmpty(searchFields.Name))
                {
                    query.AppendFormat("artistName={0}&", searchFields.Name);
                }
            }

            var url = $"/search/artists?{query}sort=relevance&p={page.ToString()}";
            return Load<Artists>(url);
        }

        public Task<Artists> SearchArtists(string artistName, int page = 1) => SearchArtists(new Artist(artistName), page);

        /// <summary>
        /// Search for a city.
        /// </summary>
        /// <param name="searchFields">
        /// You must provide a value for at least one of the following properties:
        /// <para>Name, State, StateCode, Country.Code</para>
        /// </param>
        /// <param name="page">Page number to fetch</param>
        /// <returns>A list of matching cities</returns>
        public Task<Cities> SearchCities(City searchFields, int page = 1)
        {
            var query = new StringBuilder();
            if (searchFields != null)
            {
                if (!string.IsNullOrEmpty(searchFields.Name))
                {
                    query.AppendFormat("name={0}&", searchFields.Name);
                }

                if (!string.IsNullOrEmpty(searchFields.State))
                {
                    query.AppendFormat("state={0}&", searchFields.State);
                }

                if (!string.IsNullOrEmpty(searchFields.StateCode))
                {
                    query.AppendFormat("stateCode={0}&", searchFields.StateCode);
                }

                if (searchFields.Country != null)
                {
                    if (!string.IsNullOrEmpty(searchFields.Country.Code))
                    {
                        query.AppendFormat("country={0}&", searchFields.Country.Code);
                    }
                }

            }
            
            return Load<Cities>($"/search/cities?{query}p={page}");
        }

        public Task<Countries> SearchCountries() => Load<Countries>("/search/countries");

        /// <summary>
        /// Search for setlists
        /// </summary>
        /// <param name="artistMbid">the artist's Musicbrainz Identifier (mbid)</param>
        /// <param name="artistName">the artist's name</param>
        /// <param name="artistTmid">the artist's Ticketmaster Identifier (tmid)</param>
        /// <param name="cityId">the city's geoId</param>
        /// <param name="cityName">the name of the city</param>
        /// <param name="countryCode">the country code</param>
        /// <param name="date">the date of the event</param>
        /// <param name="lastUpdated">the date and time (UTC) when this setlist was last updated - either edited or reverted. search will return setlists that were updated on or after this date</param>
        /// <param name="cityState">the state</param>
        /// <param name="cityStateCode">the state code</param>
        /// <param name="tourName">The name of the tour a setlist was a part of.</param>
        /// <param name="venueId">the venue id</param>
        /// <param name="venueName">the name of the venue</param>
        /// <param name="year">the year of the event</param>
        /// <param name="page">the number of the result page</param>
        /// <returns>a list of matching setlists</returns>
        public Task<Setlists> SearchSetlists(
            string? artistMbid = null,
            string? artistName = null,
            int? artistTmid = null,
            string? cityId = null,
            string? cityName = null,
            string? countryCode = null,
            DateTime? date = null,
            DateTime? lastUpdated = null,
            string? cityState = null,
            string? cityStateCode = null,
            string? tourName = null,
            string? venueId = null,
            string? venueName = null,
            int? year = null,
            int page = 1)
        {
            var query = new StringBuilder();
            if (tourName != null)
            {
                query.AppendFormat("tour={0}&", tourName);
            }

            if (lastUpdated.HasValue)
            {
                query.AppendFormat("lastUpdate={0::yyyyMMddHHmmss}&", lastUpdated.Value.ToUniversalTime());
            }

            if (date.HasValue)
            {
                query.AppendFormat("date={0:dd-MM-yyyy}&", date.Value.ToUniversalTime());
            }

            if (year.HasValue)
            {
                query.AppendFormat("year={0}&", year);
            }

            if (!string.IsNullOrEmpty(artistMbid))
            {
                query.AppendFormat("artistMbid={0}&", artistMbid);
            }

            if (artistTmid.HasValue)
            {
                query.AppendFormat("artistTmid={0}&", artistTmid);
            }

            if (!string.IsNullOrEmpty(artistName))
            {
                query.AppendFormat("artistName={0}&", artistName);
            }

            if (!string.IsNullOrEmpty(venueId))
            {
                query.AppendFormat("venueId={0}&", venueId);
            }

            if (!string.IsNullOrEmpty(venueName))
            {
                query.AppendFormat("venueName={0}&", venueName);
            }

            if (!string.IsNullOrEmpty(cityId) && cityId != "0")
            {
                query.AppendFormat("cityId={0}&", cityId);
            }

            if (!string.IsNullOrEmpty(cityName))
            {
                query.AppendFormat("cityName={0}&", cityName);
            }

            if (!string.IsNullOrEmpty(cityState))
            {
                query.AppendFormat("state={0}&", cityState);
            }

            if (!string.IsNullOrEmpty(cityStateCode))
            {
                query.AppendFormat("stateCode={0}&", cityStateCode);
            }

            if (!string.IsNullOrEmpty(countryCode))
            {
                query.AppendFormat("countryCode={0}&", countryCode);
            }

            if (query.Length == 0)
            {
                throw new Exception("No search criteria specified");
            }

            return Load<Setlists>($"/search/setlists?{query}p={page}");
        }

        /// <summary>
        /// Search for venues.
        /// </summary>
        /// <param name="searchFields">
        /// You must provide a value for at least one of the following properties:
        /// <para>Name, City.Id, City.Name, City.State, City.StateCode, City.Country.Code</para>
        /// </param>
        /// <param name="page">Page number to fetch</param>
        /// <returns>A list of matching venues</returns>
        public Task<Venues> SearchVenues(Venue searchFields, int page = 1)
        {
            var query = new StringBuilder();

            if (searchFields != null)
            {
                if (!string.IsNullOrEmpty(searchFields.Name))
                {
                    query.AppendFormat("name={0}&", searchFields.Name);
                }

                if (searchFields.City != null)
                {
                    if (searchFields.City.Id != "0" && searchFields.City.Id != "")
                    {
                        query.AppendFormat("cityId={0}&", searchFields.City.Id);
                    }

                    if (!string.IsNullOrEmpty(searchFields.City.Name))
                    {
                        query.AppendFormat("cityName={0}&", searchFields.City.Name);
                    }

                    if (!string.IsNullOrEmpty(searchFields.City.State))
                    {
                        query.AppendFormat("state={0}&", searchFields.City.State);
                    }

                    if (!string.IsNullOrEmpty(searchFields.City.StateCode))
                    {
                        query.AppendFormat("stateCode={0}&", searchFields.City.StateCode);
                    }

                    if (searchFields.City.Country != null)
                    {
                        if (!string.IsNullOrEmpty(searchFields.City.Country.Code))
                        {
                            query.AppendFormat("country={0}&", searchFields.City.Country.Code);
                        }
                    }
                }
            }

            return Load<Venues>($"/search/venues?{query}p={page.ToString()}");
        }

        /// <summary>
        /// Returns the current version of a setlist. E.g. if you pass the id of a setlist that got edited since you last accessed it, you'll get the current version.
        /// </summary>
        /// <param name="setlistId">Setlist id</param>
        /// <returns>The setlist for the provided id</returns>
        public Task<Setlist> Setlist(string setlistId) => Load<Setlist>($"/setlist/{setlistId}");

        public Task<User> User(string userId) => Load<User>($"/user/{userId}");

        public Task<Venue> Venue(string venueId) => Load<Venue>($"/venue/{venueId}");

        public Task<Setlists> ArtistSetlists(string mbid, int page = 1) => Load<Setlists>($"/artist/{mbid}/setlists?p={page}");

        /// <summary>
        /// Returns a setlist for the given versionId. The setlist returned isn't necessarily the most recent version.
        /// E.g. if you pass the versionId of a setlist that got edited since you last accessed it, you'll get the same version as last time.
        /// </summary>
        /// <param name="versionId">The version ID</param>
        /// <returns>The setlist for the provided versionId</returns>
        public Task<Setlist> SetlistVersion(string versionId) => Load<Setlist>($"/setlist/version/{versionId}");

        /// <summary>
        /// Get a list of setlists of concerts attended by a user.
        /// </summary>
        /// <param name="userId">A user's userId</param>
        /// <param name="page">Page number to fetch</param>
        /// <returns>A list of setlists</returns>
        public Task<Setlists> UserAttended(string userId, int page = 1) => Load<Setlists>($"/user/{userId}/attended?p={page}");

        /// <summary>
        /// Get a list of setlists of concerts edited by a user. 
        /// <para>The list contains the current version, not the version edited</para>
        /// </summary>
        /// <param name="userId">The user's userId</param>
        /// <param name="page">Page number to fetch</param>
        /// <returns>A list of setlists</returns>
        public Task<Setlists> UserEdited(string userId, int page = 1) => Load<Setlists>($"/user/{userId}/edited?p={page}");

        public Task<Setlists> VenueSetlists(string venueId, int page = 1) => Load<Setlists>($"/venue/{venueId}/setlists?p={page}");

        private async Task<T> Load<T>(string pathAndQuery)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{Host}/rest/{ApiVersion}{pathAndQuery}"));
            using var response = await _httpClient.SendAsync(request);

#pragma warning disable CS8603 // Possible null reference return.
            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}

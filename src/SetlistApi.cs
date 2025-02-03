using SetlistNet.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SetlistNet
{
    public class SetlistApi(string apiToken, string desiredLanguage = "en")
    {
        private const string Host = "https://api.setlist.fm";
        private const string ApiVersion = "1.0";

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

                if (searchFields.TMIDSpecified)
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
        /// Search for setlists.
        /// </summary>
        /// <param name="searchFields">
        /// You must provide a value for at least one of the following properties:
        /// <para>Tour, LastUpdated, EventDate (you could set only a year, e.g. <code>00-00-2007</code>),</para>
        /// <para>Artist.Name, Artist.MBID, Artist.TMID (set <code>TMIDSpecified = true</code>)</para>
        /// <para>Venue.Id, Venue.Name, Venue.City.Id, Venue.City.Name. Venue.City.State, Venue.City.State.Code, Venue.City.Country.Code</para>
        /// </param>
        /// <param name="page">Page number to fetch</param>
        /// <returns>A list of matching setlists</returns>
        public Task<Setlists> SearchSetlists(Setlist searchFields, int page = 1)
        {
            var query = new StringBuilder();
            if (searchFields != null)
            {
                if (!string.IsNullOrEmpty(searchFields.TourName))
                {
                    query.AppendFormat("tour={0}&", searchFields.Tour);
                }

                if (!string.IsNullOrEmpty(searchFields.LastUpdated))
                {
                    query.AppendFormat("lastUpdate={0}&", searchFields.LastUpdated);
                }

                if (!string.IsNullOrEmpty(searchFields.EventDate))
                {
                    if (searchFields.GetEventDateTime() != null)
                    {
                        query.AppendFormat("date={0}&", searchFields.EventDate);
                    }
                    else
                        if (searchFields.GetYear() != 0)
                    {
                        query.AppendFormat("year={0}&", searchFields.GetYear());
                    }
                }

                if (searchFields.Artist != null)
                {
                    if (!string.IsNullOrEmpty(searchFields.Artist.MBID))
                    {
                        query.AppendFormat("artistMbid={0}&", searchFields.Artist.MBID);
                    }

                    if (searchFields.Artist.TMIDSpecified)
                    {
                        query.AppendFormat("artistTmid={0}&", searchFields.Artist.TMID);
                    }

                    if (!string.IsNullOrEmpty(searchFields.Artist.Name))
                    {
                        query.AppendFormat("artistName={0}&", searchFields.Artist.Name);
                    }
                }
                if (searchFields.Venue != null)
                {
                    if (!string.IsNullOrEmpty(searchFields.Venue.Id))
                    {
                        query.AppendFormat("venueId={0}&", searchFields.Venue.Id);
                    }

                    if (!string.IsNullOrEmpty(searchFields.Venue.Name))
                    {
                        query.AppendFormat("venueName={0}&", searchFields.Venue.Name);
                    }

                    if (searchFields.Venue.City != null)
                    {
                        if (searchFields.Venue.City.Id != "0" && searchFields.Venue.City.Id != "")
                        {
                            query.AppendFormat("cityId={0}&", searchFields.Venue.City.Id);
                        }

                        if (!string.IsNullOrEmpty(searchFields.Venue.City.Name))
                        {
                            query.AppendFormat("cityName={0}&", searchFields.Venue.City.Name);
                        }

                        if (!string.IsNullOrEmpty(searchFields.Venue.City.State))
                        {
                            query.AppendFormat("state={0}&", searchFields.Venue.City.State);
                        }

                        if (!string.IsNullOrEmpty(searchFields.Venue.City.StateCode))
                        {
                            query.AppendFormat("stateCode={0}&", searchFields.Venue.City.StateCode);
                        }

                        if (searchFields.Venue.City.Country != null)
                        {
                            if (!string.IsNullOrEmpty(searchFields.Venue.City.Country.Code))
                            {
                                query.AppendFormat("countryCode={0}&", searchFields.Venue.City.Country.Code);
                            }
                        }
                    }
                }
            }

            return Load<Setlists>($"/search/setlists?{query}p={page.ToString()}");
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

        private async Task<T> Load<T>(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("x-api-key", apiToken);
            client.DefaultRequestHeaders.Add("Accept-Language", desiredLanguage);

            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(Host + "/rest/" + ApiVersion + url));

            var response = await client.SendAsync(request);

            return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
        }
    }
}

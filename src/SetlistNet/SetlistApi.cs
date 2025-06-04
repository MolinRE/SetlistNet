using SetlistNet.Exceptions;
using SetlistNet.JsonConverters;
using SetlistNet.Models;
using SetlistNet.Models.ArrayResult;
using SetlistNet.Models.Enum;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SetlistNet;

public class SetlistApi
{
    private const string Host = "https://api.setlist.fm";
    private const string ApiVersion = "1.0";
        
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _opts;

    public SetlistApi(string apiToken, string desiredLanguage = "en", HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("x-api-key", apiToken);
        _httpClient.DefaultRequestHeaders.Add("Accept-Language", desiredLanguage);
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"SetlistNet/2.1.0 ({apiToken})");

        _opts = new()
        {
            Converters = { new DateTimeConverter() }
        };
    }

    public Task<Artist> Artist(string mbid)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(mbid);
        return Load<Artist>($"/artist/{mbid}");
    }

    public Task<City> City(int geoId) => Load<City>($"/city/{geoId}");

    public Task<City> City(string geoId)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(geoId);
        return Load<City>($"/city/{geoId}");
    }

    public Task<Artists> SearchArtists(
        string? artistMbid = null,
        string? artistName = null,
        int? artistTmid = null,
        ArtistSort sort = ArtistSort.Name,
        int page = 1)
    {
        var query = new StringBuilder();

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

        if (query.Length == 0)
        {
            throw new NoSearchCriteriaSpecifiedException();
        }

        query.AppendFormat("sort={0}", sort == ArtistSort.Name ? "sortName" : "relevance");

        var url = $"/search/artists?{query}&p={page.ToString()}";
        return Load<Artists>(url);
    }
    
    public Task<Cities> SearchCities(
        string? countryCode = null,
        string? cityName = null,
        string? cityState = null,
        string? cityStateCode = null,
        int page = 1)
    {
        var query = new StringBuilder();

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
            throw new NoSearchCriteriaSpecifiedException();
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
            query.AppendFormat("lastUpdated={0:yyyyMMddHHmmss}&", lastUpdated.Value.ToUniversalTime());
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
            throw new NoSearchCriteriaSpecifiedException();
        }

        return Load<Setlists>($"/search/setlists?{query}p={page}");
    }

    /// <summary>
    /// Search for venues
    /// </summary>
    /// <param name="cityId">the city's geoId</param>
    /// <param name="cityName">name of the city where the venue is located</param>
    /// <param name="countryCode">the city's country code</param>
    /// <param name="venueName">name of the venue</param>
    /// <param name="cityState">the city's state</param>
    /// <param name="cityStateCode">the city's state code</param>
    /// <param name="page">the number of the result page you'd like to have</param>
    /// <returns>a list of matching venues</returns>
    /// <exception cref="Exception">no search criteria specified</exception>
    public Task<Venues> SearchVenues(
        string? cityId = null,
        string? cityName = null,
        string? countryCode = null,
        string? venueName = null,
        string? cityState = null,
        string? cityStateCode = null,
        int page = 1)
    {
        var query = new StringBuilder();

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
            throw new NoSearchCriteriaSpecifiedException();
        }

        return Load<Venues>($"/search/venues?{query}p={page.ToString()}");
    }

    /// <summary>
    /// Returns the current version of a setlist. E.g. if you pass the id of a setlist that got edited since you last accessed it, you'll get the current version.
    /// </summary>
    /// <param name="setlistId">Setlist id</param>
    /// <returns>The setlist for the provided id</returns>
    public Task<Setlist> Setlist(string setlistId)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(setlistId);
        return Load<Setlist>($"/setlist/{setlistId}");
    }

    public Task<User> User(string userId)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(userId);
        return Load<User>($"/user/{userId}");
    }

    public Task<Venue> Venue(string venueId)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(venueId);
        return Load<Venue>($"/venue/{venueId}");
    }

    public Task<Setlists> ArtistSetlists(string mbid, int page = 1)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(mbid);
        return Load<Setlists>($"/artist/{mbid}/setlists?p={page}");
    }

    /// <summary>
    /// Returns a setlist for the given versionId. The setlist returned isn't necessarily the most recent version.
    /// E.g. if you pass the versionId of a setlist that got edited since you last accessed it, you'll get the same version as last time.
    /// </summary>
    /// <param name="versionId">The version ID</param>
    /// <returns>The setlist for the provided versionId</returns>
    public Task<Setlist> SetlistVersion(string versionId)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(versionId);
        return Load<Setlist>($"/setlist/version/{versionId}");
    }

    /// <summary>
    /// Get a list of setlists of concerts attended by a user.
    /// </summary>
    /// <param name="userId">A user's userId</param>
    /// <param name="page">Page number to fetch</param>
    /// <returns>A list of setlists</returns>
    public Task<Setlists> UserAttended(string userId, int page = 1)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(userId);
        return Load<Setlists>($"/user/{userId}/attended?p={page}");
    }

    /// <summary>
    /// Get a list of setlists of concerts edited by a user. 
    /// <para>The list contains the current version, not the version edited</para>
    /// </summary>
    /// <param name="userId">The user's userId</param>
    /// <param name="page">Page number to fetch</param>
    /// <returns>A list of setlists</returns>
    public Task<Setlists> UserEdited(string userId, int page = 1)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(userId);
        return Load<Setlists>($"/user/{userId}/edited?p={page}");
    }

    public Task<Setlists> VenueSetlists(string venueId, int page = 1)
    {
        NoSearchCriteriaSpecifiedException.ThrowIfNullOrWhiteSpace(venueId);
        return Load<Setlists>($"/venue/{venueId}/setlists?p={page}");
    }

    private async Task<T> Load<T>(string pathAndQuery)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{Host}/rest/{ApiVersion}{pathAndQuery}"));
        using var response = await _httpClient.SendAsync(request);

#pragma warning disable CS8603 // Possible null reference return.
        return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync(), _opts);
#pragma warning restore CS8603 // Possible null reference return.
    }
}
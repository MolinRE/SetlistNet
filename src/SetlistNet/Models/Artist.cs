using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// This class represents an artist. An artist is a musician or a group of musicians. 
/// Each artist has a definite <a href="http://wiki.musicbrainz.org/MBID">Musicbrainz Identifier</a> (MBID) with which the artist can be uniquely identified.
/// <seealso cref="!:http://wiki.musicbrainz.org/MBID"/>
/// </summary>
public class Artist(string name, string mbid, string url, string sortName, string? disambiguation, int? tmid)
{
    /// <summary>
    /// Disambiguation to distinguish between artists with same names.
    /// </summary>
    [JsonPropertyName("disambiguation")]
    public string? Disambiguation { get; set; } = disambiguation;

    /// <summary>
    /// Gets or sets unique Musicbrainz Identifier (MBID)
    /// </summary>
    /// <example>"b10bbbfc-cf9e-42e0-be17-e2c3e1d2600d" (The Beatles)</example>
    [JsonPropertyName("mbid")]
    public string MBID { get; set; } = mbid;

    /// <summary>
    /// Gets or sets unique Ticket Master Identifier
    /// </summary>
    /// <example>1953</example>
    [JsonPropertyName("tmid")]
    public int? TMID { get; set; } = tmid;

    /// <summary>
    /// Gets or sets the artist's name
    /// </summary>
    /// <example>"The Beatles" or "Bruce Springsteen"</example>
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    /// <summary>
    /// Gets or sets the artist's sort name
    /// </summary>
    /// <example>"Beatles, The" or "Springsteen, Bruce"</example>
    [JsonPropertyName("sortName")]
    public string SortName { get; set; } = sortName;

    /// <summary>
    /// Gets or sets the url to artist's setlists' page on Setlist.fm
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = url;

    /// <summary>
    /// Gets the url to artist's stats' page on Setlist.fm.
    /// </summary>
    public string UrlStats => Url.Replace("/setlists/", "/stats/");

    public string GetNameWithDisambiguation()
    {
        if (string.IsNullOrEmpty(Disambiguation))
        {
            return Name;
        }

        return Name + " (" + Disambiguation + ")";
    }

    public override string ToString() => Name;
}
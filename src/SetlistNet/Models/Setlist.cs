using System;
using System.Text.Json.Serialization;

namespace SetlistNet.Models;

/// <summary>
/// This class represents a setlist. 
/// <para>
/// Please remember that setlist.fm is a wiki, so there are different versions of the same setlist. 
/// Thus, the best way to check whether two setlists are the same is to use <see cref="VersionId"/>.
/// </para>
/// </summary>
/// <remarks>
/// A setlist can be distinguished from other setlists by its unique id. But as setlist.fm works the wiki way, there can
/// be different versions of one setlist (each time a user updates a setlist a new version gets created). 
/// These different versions have a unique id on its own. So setlists can have the same id, although they differ as far 
/// as the content is concerned - thus the best way to check if two setlists are the same is to compare their versionIds.
/// </remarks>
public class Setlist(
    Artist artist,
    Venue venue,
    Tour? tour,
    Sets sets,
    string? info,
    string url,
    string id,
    string versionId,
    DateTime eventDate,
    DateTime lastUpdated)
{
    /// <summary>
    /// Gets or sets the setlist's artist.
    /// </summary>
    [JsonPropertyName("artist")]
    public Artist Artist { get; set; } = artist;

    /// <summary>
    /// Gets or sets the setlist's venue.
    /// </summary>
    [JsonPropertyName("venue")]
    public Venue Venue { get; set; } = venue;

    /// <summary>
    /// Gets or sets the tour in which the band performed setlist.
    /// </summary>
    [JsonPropertyName("tour")]
    public Tour? Tour { get; set; } = tour;

    /// <summary>
    /// Gets or sets all sets of this setlist.
    /// </summary>
    [JsonPropertyName("sets")]
    public Sets Sets { get; set; } = sets;

    /// <summary>
    /// Gets or sets additional information on the concert - see the <a href="http://www.setlist.fm/guidelines">Guidelines</a> for a complete list of allowed content
    /// <seealso cref="!:http://www.setlist.fm/guidelines"/>
    /// </summary>
    [JsonPropertyName("info")]
    public string? Info { get; set; } = info;

    /// <summary>
    /// Gets or sets the attribution url to which you have to link to wherever you use data from this setlist in your application.
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; } = url;

    /// <summary>
    /// Gets or sets unique identifier of setlist.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = id;

    /// <summary>
    /// Gets or sets unique identifier of the version of setlist.
    /// </summary>
    [JsonPropertyName("versionId")]
    public string VersionId { get; set; } = versionId;

    /// <summary>
    /// Gets or sets date of the concert
    /// </summary>
    [JsonPropertyName("eventDate")]
    public DateTime EventDate { get; set; } = eventDate;

    /// <summary>
    /// Gets or sets date, time and time zone of the last update to this setlist
    /// </summary>
    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdated { get; set; } = lastUpdated;

    public override string ToString() => $"[{EventDate:dd-MM-yyyy}] {Artist.Name} @ {Venue.Name}";
}
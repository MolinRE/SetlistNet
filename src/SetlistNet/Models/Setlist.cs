using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
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
    public class Setlist
    {
        /// <summary>
        /// Gets or sets the setlist's artist.
        /// </summary>
        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the setlist's venue.
        /// </summary>
        [JsonPropertyName("venue")]
        public Venue Venue { get; set; }

        /// <summary>
        /// Gets or sets the tour in which the band performed setlist.
        /// </summary>
        [JsonPropertyName("tour")]
        public Tour Tour { get; set; }

        /// <summary>
        /// Gets or sets all sets of this setlist.
        /// </summary>
        [JsonPropertyName("sets")]
        public Sets Sets { get; set; }

        /// <summary>
        /// Gets or sets additional information on the concert - see the Guidelines for a complete list of allowed content.
        /// <para>See: <see cref="http://www.setlist.fm/guidelines"/></para>
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; }

        /// <summary>
        /// Gets or sets the attribution url to which you have to link to wherever you use data from this setlist in your application.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of setlist.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of the version of setlist.
        /// </summary>
        [JsonPropertyName("versionId")]
        public string VersionId { get; set; }

        /// <summary>
        /// Gets or sets date of the concert in the format "dd-MM-yyyy", e.g. 31-03-2007.
        /// </summary>
        [JsonPropertyName("eventDate")]
        public DateTime EventDate { get; set; }

        /// <summary>
        /// Gets or sets date, time and time zone of the last update to this setlist in the format "yyyy-MM-dd'T'HH:mm:ss.SSSZZZZZ".
        /// </summary>
        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; }

        public Setlist()
        {

        }

        public Setlist(Artist artist)
        {
            Artist = artist;
        }

        public override string ToString() => $"[{EventDate}] {Artist.Name} @ {Venue.Name}";
    }
}

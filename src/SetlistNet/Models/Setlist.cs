using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a setlist. 
    /// <para>
    /// Please remember that setlist.fm is a wiki, so there are different versions of the same setlist. 
    /// Thus, the best way to check whether two setlists are the same is to use <paramref name="VersionId"/>.
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

        [JsonIgnore]
        public string? TourName
        {
            get => Tour?.Name;
            set
            {
                Tour ??= new();

                Tour.Name = value;
            }
        }

        /// <summary>
        /// Gets or sets all sets of this setlist.
        /// </summary>
        [JsonIgnore]
        public List<Set> Sets { get; set; }

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
        public string EventDate { get; set; }

        /// <summary>
        /// Gets or sets date, time and time zone of the last update to this setlist in the format "yyyy-MM-dd'T'HH:mm:ss.SSSZZZZZ".
        /// </summary>
        [JsonPropertyName("lastUpdated")]
        public string LastUpdated { get; set; }

        // [JsonExtensionData]
        // private IDictionary<string, J> _additionalData;
        //
        // [OnDeserialized]
        // private void OnDeserialized(StreamingContext context)
        // {
        //     Sets = _additionalData["sets"].First.First.ToObject<List<Set>>();
        // }

        public Setlist()
        {

        }

        public Setlist(Artist artist)
        {
            Artist = artist;
        }

        public DateTime? GetEventDateTime()
        {
            if (DateTime.TryParseExact(EventDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
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
            if (EventDate is { Length: 10 })
            {
                return Convert.ToUInt16(EventDate[6..]);
            }

            return 0;
        }

        public override string ToString()
        {
            return $"[{EventDate}] {Artist.Name} @ {Venue.Name}";
        }
    }
}

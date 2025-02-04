using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a song that is part of a <see cref="Set"/>.
    /// </summary>
    public class Song
    {
        /// <summary>
        /// The name of the song
        /// </summary>
        /// <example>Yesterday or "Wish You Were Here"</example>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// A different <see cref="Artist"/> than the performing one that joined the stage for this song.
        /// </summary>
        [JsonPropertyName("with")]
        public Artist With { get; set; }

        /// <summary>
        /// The original <see cref="Artist"/> of this song, if different to the performing artist.
        /// </summary>
        [JsonPropertyName("cover")]
        public Artist Cover { get; set; }

        /// <summary>
        /// Special incidents or additional information about the way the song was performed at this specific concert. See the <a href="https://www.setlist.fm/guidelines">setlist.fm guidelines</a> complete list of allowed content.
        /// </summary>
        [JsonPropertyName("info")]
        public string Info { get; set; }
        
        /// <summary>
        /// The song came from tape rather than being performed live. See the <a href="https://www.setlist.fm/guidelines#tape-songs">tape section of the guidelines</a> for valid usage.
        /// </summary>
        [JsonPropertyName("tape")]
        public bool Tape { get; set; }

        /// <summary>
        /// Returns the <see cref="Name"/> propertu of the object.
        /// </summary>
        /// <returns>A string that represents <see cref="Name"/> property</returns>
        public override string ToString() => Name;
    }
}

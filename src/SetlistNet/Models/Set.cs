using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// A setlist consists of different (at least one) sets. Sets can either be sets as defined in the Guidelines or encores.
    /// <para>See: <see cref="http://www.setlist.fm/guidelines"/></para>
    /// </summary>
    public class Set
    {
        /// <summary>
        /// If the set is an encore, this property gets or sets the number of the encore, starting with 1 for the first encore, 2 for the second and so on.
        /// </summary>
        [JsonPropertyName("encore")]
        public int? Encore { get; set; }

        /// <summary>
        /// Gets or sets the description/name of the set. E.g. "Acoustic set" or "Paul McCartney solo".
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets this set's songs.
        /// </summary>
        [JsonPropertyName("song")]
        public List<Song> Songs { get; set; }

        public override string ToString()
        {
            var encore = Encore.HasValue ? "[Encore " + Encore + "] " : string.Empty;

            return $"{encore}{Name}";
        }
    }
}

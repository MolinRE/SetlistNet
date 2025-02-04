﻿using System.Text.Json.Serialization;

namespace SetlistNet.Models
{
    /// <summary>
    /// This class represents a user.
    /// </summary>
    public class User
    {
        [JsonPropertyName("flickr")]
        public string Flickr { get; set; }

        [JsonPropertyName("twitter")]
        public string Twitter { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("lastFm")]
        public string LastFm { get; set; }

        [JsonPropertyName("mySpace")]
        public string MySpace { get; set; }

        [JsonPropertyName("fullname")]
        public string Fullname { get; set; }

        [JsonPropertyName("about")]
        public string About { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        public override string ToString() => UserId;
    }
}

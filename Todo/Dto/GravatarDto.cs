using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Todo.Dto
{
    public class Photo
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("givenName")]
        public string GivenName { get; set; }

        [JsonPropertyName("familyName")]
        public string FamilyName { get; set; }

        [JsonPropertyName("formatted")]
        public string Formatted { get; set; }
    }

    public class Entry
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("requestHash")]
        public string RequestHash { get; set; }

        [JsonPropertyName("profileUrl")]
        public string ProfileUrl { get; set; }

        [JsonPropertyName("preferredUsername")]
        public string PreferredUsername { get; set; }

        [JsonPropertyName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonPropertyName("photos")]
        public List<Photo> Photos { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("urls")]
        public List<object> Urls { get; set; }
    }

    public class GravatarDto
    {
        [JsonPropertyName("entry")]
        public List<Entry> Entry { get; set; }
    }


}

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace social_media_app.Models
{
    [Table("users")]
    public class User
    {
        public int Id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? gender { get; set; }
        public List<int>? followers { get; set; } = new List<int>();
        public List<int>? following { get; set; } = new List<int>();

        [JsonIgnore]
        public List<Post>? savedPost { get; set; } = new List<Post>();
        
        [JsonIgnore]
        public List<Post>? likedPost { get; set; } = new List<Post>();
        [JsonIgnore] public List<Post> posts { get; set; } = new List<Post>();
        [JsonIgnore] public List<Chat> chats { get; set; } = new List<Chat>();
        [JsonIgnore] public List<Message> messages { get; set; } = new List<Message>();
        [JsonIgnore] public List<Comment> comments { get; set; } = new List<Comment>();
        [JsonIgnore] public List<Comment> likedComment { get; set; } = new List<Comment>();
        [JsonIgnore] public List<Story> stories { get; set; } = new List<Story>();
        [JsonIgnore] public List<Reels> reels { get; set; } = new List<Reels>();
    }
}

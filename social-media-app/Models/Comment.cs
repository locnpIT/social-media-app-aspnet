using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace social_media_app.Models
{
    [Table("comments")]
    public class Comment
    {
        [Key] public int Id { get; set; }
        public string? content { get; set; }
        public User? user { get; set; }
        [JsonIgnore]public List<User> liked { get; set; } = new List<User>();
        [JsonIgnore]public List<Post> posts { get; set; } = new List<Post>();
        public DateTime createAt { get; set; }
    }
}

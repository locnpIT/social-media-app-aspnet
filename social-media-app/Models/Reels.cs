using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    [Table("reels")]
    public class Reels
    {
        [Key] public long Id { get; set; }
        public string? title { get; set; }
        public string? video { get; set; }
        public User? user { get; set; }
    }
}

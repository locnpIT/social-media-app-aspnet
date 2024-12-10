using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    [Table("stories")]
    public class Story
    {
        [Key] public int Id { get; set; }
        public string? image {  get; set; }
        public string? caption { get; set; }
        public DateTime? timestamp { get; set; }
        public User? user { get; set; }
    }
}

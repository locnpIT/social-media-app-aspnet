using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{
    [Table("chats")]
    public class Chat
    {
        [Key] public int Id { get; set; }
        public string? chat_name { get; set; }
        public string? chat_image { get; set; }
        public List<User> users { get; set; } = new List<User>();
        public DateTime timestamp { get; set; }
        public List<Message> messages { get; set; } = new List<Message>();
    }
}

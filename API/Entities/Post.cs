using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Posts")]
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
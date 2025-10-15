namespace API.Entities
{
    public class Friend
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public User FriendUser { get; set; }
        public int FriendUserId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}

namespace API.Entities
{
    public class UserInvitation
    {
        public User SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public User TargetUser { get; set; }
        public int TargetUserId { get; set; }
    }
}
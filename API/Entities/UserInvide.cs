namespace API.Entities
{
    public class UserInvide
    {
        public User SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public User TargetUser { get; set; }
        public int TargetUserId { get; set; }
    }
}
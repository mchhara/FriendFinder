using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class Role : IdentityRole<int>
    {
        ICollection<UserRole> UserRoles { get; set; }
        
    }
}
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager) 
        {
            if(await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};

            var users = JsonSerializer.Deserialize<List<User>>(userData);

            var roles = new List<Role>
            {
                new Role{Name = "Member"},
                new Role{Name = "Admin"},
                new Role{Name = "Moderator"}
            };

            foreach(var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach(var user in users)
            {
                user.Photos.First().IsApproved = true;
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new User
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
             await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});

        }
    }
}
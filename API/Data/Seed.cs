using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<User> userManager)
        {
            if(await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};

            var users = JsonSerializer.Deserialize<List<User>>(userData);

            foreach(var user in users)
            {
                user.UserName = user.UserName.ToLower();

                await userManager.CreateAsync(user, "Pas$$w0rd");
            }
        }
    }
}
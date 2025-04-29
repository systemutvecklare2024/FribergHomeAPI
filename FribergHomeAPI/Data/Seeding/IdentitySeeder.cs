using FribergHomeAPI.Constants;
using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FribergHomeAPI.Data.Seeding
{
    // Author: Christoffer
    public static class IdentitySeeder
    {
        private const string UserID = "c9bef39b-80e8-4f5f-81bd-88ba728ff5de";
        private const string AgentID = "e9084259-40fb-4cc6-ab79-5f66a448635d";
        private const string SuperAgentID = "1b427517-3b8b-43eb-9d94-7fb7a55dade3";
        private const string AdministratorID = "bdd62857-84b2-4024-9166-d6a9540027bb";

        public record NewUser(string Id, string Email, string UserName, string FirstName, string LastName, string Password);


        public static async Task SeedAsync(ApplicationDbContext ctx, RoleManager<IdentityRole> roleManager, UserManager<ApiUser> userManager)
        {
            await SeedRoles(ctx, roleManager);
            await SeedDefaultUsers(ctx, userManager);
        }

        private static async Task SeedDefaultUsers(ApplicationDbContext ctx, UserManager<ApiUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                await CreateUser(new NewUser(UserID, "user@friberghome.com", "user@friberghome.com", "Normal", "User", "User123!"), ApiRoles.User, userManager);
                await CreateUser(new NewUser(AdministratorID, "admin@friberghome.com", "admin@friberghome.com", "Normal", "Admin", "Admin123!"), ApiRoles.Administrator, userManager);
            }
        }

        public static async Task SeedRoles(ApplicationDbContext ctx, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[]
            {
                new IdentityRole
                {
                    Name = ApiRoles.User,
                    NormalizedName = ApiRoles.User.ToUpper(),
                    Id = ApiRoles.UserGUID
                },
                new IdentityRole
                {
                    Name = ApiRoles.Agent,
                    NormalizedName = ApiRoles.Agent.ToUpper(),
                    Id = ApiRoles.AgentGUID
                },
                new IdentityRole
                {
                    Name = ApiRoles.SuperAgent,
                    NormalizedName = ApiRoles.SuperAgent.ToUpper(),
                    Id = ApiRoles.SuperAgentGUID
                },
                new IdentityRole{
                    Name = ApiRoles.Administrator,
                    NormalizedName = ApiRoles.Administrator.ToUpper(),
                    Id = ApiRoles.AdministratorGUID
                },
            };

            foreach (var role in roles)
            {
                var exists = await roleManager.RoleExistsAsync(role.Name);
                if (!exists)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }

        public static async Task CreateUser(NewUser newUser, string role, UserManager<ApiUser> userManager)
        {
            var user = new ApiUser
            {
                Id = newUser.Id,
                Email = newUser.Email,
                NormalizedEmail = newUser.Email.ToUpper(),
                UserName = newUser.UserName,
                NormalizedUserName = newUser.UserName.ToUpper(),
                FirstName = newUser.FirstName,
                LastName = newUser.LastName
            };

            var userResult = await userManager.CreateAsync(user, newUser.Password);
            if (userResult.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}

using FribergHomeAPI.Constants;
using FribergHomeAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FribergHomeAPI.Data.Seeding
{
    public static class IdentitySeeder
    {
        private const string UserID = "c9bef39b-80e8-4f5f-81bd-88ba728ff5de";
        private const string AgentID = "e9084259-40fb-4cc6-ab79-5f66a448635d";
        private const string SuperAgentID = "1b427517-3b8b-43eb-9d94-7fb7a55dade3";
        private const string AdministratorID = "bdd62857-84b2-4024-9166-d6a9540027bb";

        
        public static async Task SeedAsync(ApplicationDbContext ctx, RoleManager<IdentityRole> roleManager, UserManager<ApiUser> userManager)
        {
            await SeedRoles(ctx, roleManager);
            await SeedDefaultUsers(ctx, userManager);
        }

        private static async Task SeedDefaultUsers(ApplicationDbContext ctx, UserManager<ApiUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new ApiUser
                {
                    Id = UserID,
                    Email = "user@friberghome.com",
                    NormalizedEmail = "user@friberghome.com".ToUpper(),
                    UserName = "user@friberghome.com",
                    NormalizedUserName = "user@friberghome.com".ToUpper(),
                    FirstName = "Normal",
                    LastName = "User",
                    //SecurityStamp = "a26183dc-1ccf-4beb-a2a3-82f8b9baa7f3",
                    //ConcurrencyStamp = "4d822a9f-bbdc-411d-b55e-df4f5610d8cb"
                };

                var userResult = await userManager.CreateAsync(user, "User123!");
                if (userResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, ApiRoles.User);
                }

                var admin = new ApiUser
                {
                    Id = AdministratorID,
                    Email = "admin@friberghome.com",
                    NormalizedEmail = "admin@friberghome.com".ToUpper(),
                    UserName = "admin@friberghome.com",
                    NormalizedUserName = "admin@friberghome.com".ToUpper(),
                    FirstName = "Normal",
                    LastName = "Admin",
                    //SecurityStamp = "a26183dc-1ccf-4beb-a2a3-82f8b9baa7f3",
                    //ConcurrencyStamp = "4d822a9f-bbdc-411d-b55e-df4f5610d8cb"
                };

                var adminResult = await userManager.CreateAsync(admin, "Admin123!");
                if (adminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, ApiRoles.Administrator);
                }
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
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovaClubWebApp.MovaClubDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovaClubWebApp.Data
{
    public class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            string[] roleNames = { "Admin", "Student", "Teacher", "Owner" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (roleResult.Succeeded == false)
                    {
                        throw new Exception();
                    }
                }
            }
            var _user = await userManager.FindByEmailAsync(configuration["AppSettings:OwnerUserEmail"]);

            if (_user == null)
            {
                var ownerUser = new AppUser
                {
                    UserName = configuration["AppSettings:OwnerUserName"],
                    Email = configuration["AppSettings:OwnerUserEmail"]

                };
                string ownerUserPWD = configuration["AppSettings:OwnerUserPassword"];
                var createOwnerUser = await userManager.CreateAsync(ownerUser, ownerUserPWD);
                if (createOwnerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(ownerUser, "Owner");
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}

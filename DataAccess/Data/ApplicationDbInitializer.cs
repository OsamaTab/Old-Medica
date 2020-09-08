using DataAccess.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "Admin".ToUpper();
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Doctors").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Doctors";
                role.NormalizedName = "Doctors".ToUpper();
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "User".ToUpper();
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@i.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@i.com",
                    Email = "admin@i.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Qwe123/").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
            for (int i = 1; i <= 12; i++)
            {
                string userName = "Doctor{0}";
                string email = "Doctor{0}@i.com";
                string userNames = string.Format(userName, i);
                string emails = string.Format(email, i);
                if (userManager.FindByEmailAsync(emails).Result == null)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = userNames,
                        Email = emails
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Qwe123/").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Doctors").Wait();
                    }
                }
            }
        }
     
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OnlineStore.Models;
using OnlineStore.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.DB
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            //string adminEmail = "admin@gmail.com";
            //string password = "a";

            //string[] usersEmails =
            //{
            //    "u1@gmail.com", "u2@gmail.com", "u3@gmail.com", "u4@gmail.com", "u5@gmail.com", "u6@gmail.com"
            //};

            //string[] usersPasswords =
            //{
            //    "1",  "2", "3", "4", "5", "6",
            //};

            string adminEmail = configuration.GetSection("AdminEmail").Value;
            string adminPassword = configuration.GetSection("AdminPassword").Value;


            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };

                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }


                //for(int i = 0; i < 6; i++)
                //{
                //    User user = new User { Email = usersEmails[i], UserName = usersEmails[i] };

                //    IdentityResult res = await userManager.CreateAsync(user, usersPasswords[i]);
                //    if (res.Succeeded)
                //    {
                //        await userManager.AddToRoleAsync(user, "user");
                //    }
                //}
            }
        }
    }
}

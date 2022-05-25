using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class RoleSeed
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "Admin_12345";

            if(await roleManager.FindByNameAsync("system_admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("system_admin"));
            }
            if(await roleManager.FindByNameAsync("establishment_seller") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("establishment_seller"));
            }
            if(await roleManager.FindByNameAsync("establishment_admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("establishment_admin"));
            }

            if(await userManager.FindByEmailAsync(adminEmail) == null)
            {
                AppUser admin = new AppUser { Email = adminEmail, UserName = adminEmail };
                var result = await userManager.CreateAsync(admin, password);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "system_admin");
                }
            }
        }
    }
}
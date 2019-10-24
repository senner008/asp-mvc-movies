using Castle.Core.Configuration;
using Microsoft.AspNetCore.Identity;

public static class ApplicationDbInitializer
{
    public static void SeedUsers(UserManager<IdentityUser> userManager, string password)
    {
        if (userManager.FindByEmailAsync("admin@email.com").Result == null)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = "admin@email.com",
                Email = "admin@email.com",
                EmailConfirmed = true
                
            };
            IdentityResult result = userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }       
    }   
    
}
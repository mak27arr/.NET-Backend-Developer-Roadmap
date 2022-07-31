using ASPNetCoreMVC.Areas.Admin.Models.User;
using ASPNetCoreMVC.Enums.Indentity;
using Microsoft.AspNetCore.Identity;

namespace ASPNetCoreMVC.Data.Indentity
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
                await roleManager.CreateAsync(new IdentityRole(role.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin@email.com",
                Email = "superadmin@email.com",
                FirstName = "FN",
                LastName = "LN",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (await userManager.FindByEmailAsync(defaultUser.Email) != null)
                return;

            await userManager.CreateAsync(defaultUser, "sadmin.");

            foreach (var role in Enum.GetValues(typeof(Roles)))
                await userManager.AddToRoleAsync(defaultUser, role.ToString());

        }
    }
}

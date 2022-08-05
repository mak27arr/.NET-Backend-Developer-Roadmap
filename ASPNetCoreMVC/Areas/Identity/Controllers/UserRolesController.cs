using ASPNetCoreMVC.Areas.Admin.Models.User;
using ASPNetCoreMVC.Areas.Identity.Models;
using ASPNetCoreMVC.Enums.Indentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreMVC.Areas.Identity.Controllers
{
    [Authorize(Roles = nameof(Roles.SuperAdmin))]
    [Area("Identity")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();

            foreach (var user in _userManager.Users)
                userRolesViewModel.Add(new ()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = await GetUserRoles(user)
                });

            return View(userRolesViewModel);
        }

        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();

            foreach (var role in _roleManager.Roles)
                model.Add(new ()
                {
                    UserId = userId,
                    RoleId = role.Id,
                    RoleName = role.Name,
                    Selected = await _userManager.IsInRoleAsync(user, role.Name)
                });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model)
        {
            var userId = model.Select(x => x.UserId).Distinct();

            if (userId.Count() > 1)
            {
                ModelState.AddModelError("", "Multiplay users not allowed now");
                return View(model);
            }


            var user = await _userManager.FindByIdAsync(userId.First());
            if (user == null)
                return RedirectToAction("Index");

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<string>> GetUserRoles(ApplicationUser user) => (await _userManager.GetRolesAsync(user)).ToList();
    }
}

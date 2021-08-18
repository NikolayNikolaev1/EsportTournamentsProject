namespace EsportsTournaments.Web.Areas.Admin.Controllers
{
    using Core.Extensions;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Users;
    using Services.Admin;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.WebConstants;

    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService users;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public UsersController(
            IAdminUserService users,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            this.users = users;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // Lists all users info in a table with option to change role.
        public async Task<IActionResult> Index()
        {
            var users = await this.users.AllAsync();
            var roles = this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(new UsersListingsViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        // Changes role of user.
        [HttpPost]
        public async Task<IActionResult> ChangeRole(ChangeUserRoleFormModel model)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (model.CurrentRole != null)
            {
                // Checks if user has any role assigned and removes it.
                await this.userManager.RemoveFromRoleAsync(user, model.CurrentRole);
            }

            await this.userManager.AddToRoleAsync(user, model.Role);

            TempData.AddSuccessMessage(string.Format(SuccessMessages.AddedUserRole, user.UserName, model.Role));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(RemoveUserRoleFormModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var userExists = user != null;

            if (!userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.RemoveFromRoleAsync(user, model.CurrentRole);

            TempData.AddSuccessMessage(string.Format(SuccessMessages.RemovedUserRole, user.UserName));
            return RedirectToAction(nameof(Index));
        }
    }
}

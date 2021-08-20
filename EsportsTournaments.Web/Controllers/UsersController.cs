namespace EsportsTournaments.Web.Controllers
{
    using Data.Models;
    using EsportsTournaments.Web.Models.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private IUserService users;
        private UserManager<User> userManager;

        public UsersController(IUserService users, UserManager<User> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        // User profile.
        [Route("/profile/{username}")]
        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return this.View(new UserProfileViewModel
            {
                UserInfo = await this.users.ProfileAsync(user.Id),
                //CaptainTeams = await this.users.GetAllCreatedTeamsAsync(user.Id)
            });
        }
    }
}

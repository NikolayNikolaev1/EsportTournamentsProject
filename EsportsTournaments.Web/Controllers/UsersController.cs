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

            var userId = user.Id;

            return this.View(new UserProfileViewModel
            {
                UserInfo = await this.users.ProfileAsync(userId),
                CreatedTeams = await this.users.GetAllCreatedTeamsListAsync(userId),
                JoinedTeams =  await this.users.GetAllJoinedTeamsListAsync(userId)
            });
        }
    }
}

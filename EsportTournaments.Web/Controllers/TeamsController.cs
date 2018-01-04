using EsportTournaments.Core.Extensions;
using EsportTournaments.Data.Models;
using EsportTournaments.Services;
using EsportTournaments.Web.Models.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EsportTournaments.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITeamService teams;
        private readonly IGameService games;

        public TeamsController(ITeamService teams, IGameService games, UserManager<User> userManager)
        {
            this.teams = teams;
            this.games = games;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int page = 1)
            => View(new TeamListingViewModel
            {
                Teams = await this.teams.AllAsync(page),
                TotalTournaments = await this.teams.TotalAsync(),
                CurrentPage = page
            });

        public async Task<IActionResult> Details(int id)
        {
            var model = new TeamDetailsViewModel
            {
                Team = await this.teams.ByIdAsync(id)
            };

            if (model.Team == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);

                model.UserIsInTeam = await this.teams.UserIsInTeamAsync(id, userId);
            }

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var userId = this.userManager.GetUserId(User);

            if (userId == null)
            {
                return BadRequest();
            }

            var games = await this
                .games
                .GetAllGamesAsync();

            var gamesListItems = games
                .Select(g => new SelectListItem
                {
                    Text = g.Name,
                    Value = g.Id.ToString()
                })
                .ToList();

            return View(new CreateTeamViewModel
            {
                Games = gamesListItems
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeamViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.teams.CreateAsync(
                    model.Name,
                    model.Tag,
                    model.TeamImageUrl,
                    userId,
                    model.GameId.ToString());

            TempData.AddSuccessMessage($"Team {model.Name} created.");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var result = await this.teams.PlayerJoinAsync(id, userId);

            if (!result)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Successfully joined team!");

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            var userId = this.userManager.GetUserId(User);

            var result = await this.teams.PlayerLeaveAsync(id, userId);

            if (!result)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Successfully left team!");

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}

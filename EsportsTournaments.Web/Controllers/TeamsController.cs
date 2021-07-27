namespace EsportsTournaments.Web.Controllers
{
    using Core.Extensions;
    using Core.Filters;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Teams;
    using Services;
    using System.Threading.Tasks;

    using static Common.WebConstants;

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

        [Authorize]
        public async Task<IActionResult> Create()
            => View(new CreateTeamViewModel
            {
                Games = await this.games.AllToSelectListAsync()
            });

        [Authorize]
        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(CreateTeamViewModel model)
        {
            var userId = this.userManager.GetUserId(User);

            if (await this.teams.ContainsNameAsync(model.Name))
            {
                ModelState.AddModelError("Name",
                    string.Format(ErrorMessages.TeamNameExists, model.Name));
                model.Games = await this.games.AllToSelectListAsync();
                return View(model);
            }

            if (await this.teams.ContainsTagAsync(model.Tag))
            {
                ModelState.AddModelError("Tag",
                    string.Format(ErrorMessages.TeamTagExists, model.Tag));
                model.Games = await this.games.AllToSelectListAsync();
                return View(model);
            }

            await this.teams.CreateAsync(
                    model.Name,
                    model.Tag,
                    model.TeamImageUrl,
                    userId,
                    model.GameId);

            TempData.AddSuccessMessage(string.Format(SuccessMessages.CreateTeam, model.Name));

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await this.teams.ContainsAsync(id))
            {
                return NotFound();
            }

            var model = new TeamDetailsViewModel
            {
                Team = await this.teams.DetailsAsync(id)
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);
                model.UserIsInTeam = await this.teams.HasPlayerAsync(id, userId);
            }

            return View(model);
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(new TeamListingViewModel
            {
                Teams = await this.teams.AllAsync(page),
                TotalTeams = await this.teams.TotalAsync(),
                CurrentPage = page
            });

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            if (!await this.teams.ContainsAsync(id))
            {
                return NotFound();
            }

            var userId = this.userManager.GetUserId(User);

            if (await this.teams.HasPlayerAsync(id, userId))
            {
                return BadRequest();
            }

            await this.teams.JoinAsync(id, userId);

            TempData.AddSuccessMessage(SuccessMessages.TeamJoined);

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (!await this.teams.ContainsAsync(id))
            {
                return NotFound();
            }

            var userId = this.userManager.GetUserId(User);

            if (!await this.teams.HasPlayerAsync(id, userId))
            {
                return BadRequest();
            }

            await this.teams.LeaveAsync(id, userId);

            TempData.AddSuccessMessage(SuccessMessages.TeamLeft);

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}

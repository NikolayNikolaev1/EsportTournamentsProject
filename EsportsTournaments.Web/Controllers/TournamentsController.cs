namespace EsportsTournaments.Web.Controllers
{
    using Core.Extensions;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Tournaments;
    using Services;
    using System.Linq;
    using System.Threading.Tasks;

    public class TournamentsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITournamentService tournaments;
        private readonly IUserService users;

        public TournamentsController(
            UserManager<User> userManager,
            ITournamentService tournaments,
            IUserService users)
        {
            this.tournaments = tournaments;
            this.userManager = userManager;
            this.users = users;
        }

        public async Task<IActionResult> Details(int id)
            => this.ViewOrNotFound(new TournamentsDetailsViewModel
            {
                Tournament = await this.tournaments.DetailsAsync(id),
                Teams = await this.tournaments.GetTeamsAsync(id)
            });

        public async Task<IActionResult> Index(int page = 1)
            => View(new TournamentListingViewModel
            {
                Tournaments = await this.tournaments.AllAsync(page),
                TotalTournaments = await this.tournaments.TotalAsync(),
                CurrentPage = page
            });

        [Authorize]
        public async Task<IActionResult> Join(int id)
        {
            var userId = this.userManager.GetUserId(User);
            var gameId = await this.tournaments.GetTournamentGameId(id);

            var teams = await this.users
                .GetAllCreatedTeamsAsync(userId);

            var teamsListItems = teams
                    .Select(t => new SelectListItem
                    {
                        Text = t.Name,
                        Value = t.Id.ToString()
                    })
                    .ToList();

            return View(new JoinTournamentViewModel
            {
                Teams = teamsListItems,
                Id = id
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Join(JoinTournamentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await this.tournaments.TeamJoin(model.Id, (int)model.TeamId);

            if (!result)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Successfully joined tournament!");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Leave(JoinTournamentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await this.tournaments.TeamLeave(model.Id, (int)model.TeamId);

            if (!result)
            {
                return BadRequest();
            }

            TempData.AddSuccessMessage("Successfully left tournament!");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }
    }
}

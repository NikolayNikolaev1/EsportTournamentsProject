using EsportTournaments.Core.Extensions;
using EsportTournaments.Data.Models;
using EsportTournaments.Services;
using EsportTournaments.Web.Models.Tournaments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EsportTournaments.Web.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITournamentService tournaments;
        private readonly IUserService users;

        public TournamentsController(ITournamentService tournaments, UserManager<User> userManager, IUserService users)
        {
            this.tournaments = tournaments;
            this.userManager = userManager;
            this.users = users;
        }

        public async Task<IActionResult> Details(int id)
        {
            return this.ViewOrNotFound(new TournamentsDetailsViewModel
            {
                Tournament = await this.tournaments.ById(id)
            });
        }

        public async Task<IActionResult> Join(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction(nameof(AccountController.Login), "Account", new { area = string.Empty });
            }

            var userId = this.userManager.GetUserId(User);
            var gameId = await this.tournaments.GetTournamentGameId(id);

            var teams = await this.users
                .GetAllCreatedTeamsAsync(gameId, userId);

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

        public async Task<IActionResult> Index(int page = 1)
            => View(new TournamentListingViewModel
            {
                Tournaments = await this.tournaments.AllAsync(page),
                TotalTournaments = await this.tournaments.TotalAsync(),
                CurrentPage = page
            });

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

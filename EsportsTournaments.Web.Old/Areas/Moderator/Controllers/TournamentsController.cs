namespace EsportsTournaments.Web.Areas.Moderator.Controllers
{
    using Core.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Tournaments;
    using Services;
    using Services.Moderator;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Controllers;

    using static Core.WebConstants;

    [Area(ModeratorArea)]
    [Authorize(Roles = TournamentModeratorRole)]
    public class TournamentsController : Controller
    {
        private readonly IModeratorTournamentService tournaments;
        private readonly IGameService games;

        public TournamentsController(IModeratorTournamentService tournaments, IGameService games)
        {
            this.tournaments = tournaments;
            this.games = games;
        }

        public async Task<IActionResult> Create()
        {
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

            return View(new CreateTournamentFormModel
            {
                Games = gamesListItems
            });
        }

        [Authorize]
        public async Task<IActionResult> Manage(int id)
        {
            var teams = await this.tournaments
                    .GetTeamsInTournamentAsync(id);

            var tournament = await this.tournaments
                .GetTournamentAsync(id);

            var teamsListItems = teams
                    .Select(t => new SelectListItem
                    {
                        Text = t.Name,
                        Value = t.Id.ToString()
                    })
                    .ToList();

            return View(new ManageTournamentViewModel
            {
                Teams = teamsListItems,
                Id = id,
                Tournament = tournament
            });
        }

        [Authorize]
        public async Task<IActionResult> End(int id)
        {
            var teams = await this.tournaments
                    .GetTeamsInTournamentAsync(id);


            var teamsListItems = teams
                    .Select(t => new SelectListItem
                    {
                        Text = t.Name,
                        Value = t.Id.ToString()
                    })
                    .ToList();

            return View(new EndTournamentViewModel
            {
                Teams = teamsListItems,
                Id = id,
            });
        }

        public async Task<IActionResult> Start(int id)
        {
            var result = await this.tournaments.StartAsync(id);
            
            if (!result)
            {
                TempData.AddSuccessMessage("Tournament already started!");
            }

            TempData.AddSuccessMessage("Successfully started tournament!");
            

            return RedirectToAction(nameof(TournamentsController.Manage), "Tournaments", new { area = ModeratorArea, id });
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //if (model.StartDate < DateTime.UtcNow)
            //{
            //    return View(model);
            //}

            //if (model.StartDate < DateTime.UtcNow)
            //{
            //    return View(model);
            //}

            TempData.AddSuccessMessage($"Successfully created tournament {model.Name}");

            await this.tournaments.CreateAsync(
                model.Name,
                model.Prize,
                model.StartDate,
                model.GameId.ToString());

            return this.RedirectToAction(
                nameof(Web
                    .Controllers
                    .TournamentsController
                    .Index),
                "Tournaments", new { area = string.Empty });
        }

        [HttpPost]
        public async Task<IActionResult> End(EndTournamentViewModel model)
        {
            var result = await this.tournaments
                    .EndTournamentAndChooseAWinner(model.Id, model.TeamId);

            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            TempData.AddSuccessMessage("Successfully started tournament!");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }
    }
}

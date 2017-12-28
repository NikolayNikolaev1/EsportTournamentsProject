using EsportTournaments.Core.Extensions;
using EsportTournaments.Services.Moderator;
using EsportTournaments.Web.Areas.Moderator.Models.Tournaments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using static EsportTournaments.Core.WebConstants;

namespace EsportTournaments.Web.Areas.Moderator.Controllers
{
    [Area(ModeratorArea)]
    [Authorize(Roles = TournamentModeratorRole)]
    public class TournamentsController : Controller
    {
        private readonly IModeratorTournamentService tournaments;

        public TournamentsController(IModeratorTournamentService tournaments)
        {
            this.tournaments = tournaments;
        }

        public async Task<IActionResult> Create()
        {
            var games = await this
                .tournaments
                .GetAllGamesForTournamentAsync();

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


        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.StartDate < DateTime.UtcNow)
            {
                return View(model);
            }

            TempData.AddSuccessMessage($"Successfully created tournament {model.Name}");

            await this.tournaments.CreateAsync(
                model.Name,
                model.Prize,
                model.StartDate,
                model.GameId.ToString());

            return this.View();//RedirectToAction(nameof(Web.Controllers.TournamentsController.Index), "Tournaments", new { area = string.Empty });
        }
    }
}

using EsportTournaments.Core.Extensions;
using EsportTournaments.Services;
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

            return this.RedirectToAction(
                nameof(Web
                    .Controllers
                    .TournamentsController
                    .Index),
                "Tournaments", new { area = string.Empty });
        }

        //[HttpPost]
        //public async Task<IActionResult> Remove(int id)
        //{
        //    var tournamentId = 
        //}
    }
}

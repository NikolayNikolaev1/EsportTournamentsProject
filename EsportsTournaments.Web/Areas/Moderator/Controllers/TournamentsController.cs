namespace EsportsTournaments.Web.Areas.Moderator.Controllers
{
    using Core.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Tournaments;
    using Services;
    using Services.Moderator;
    using System;
    using System.Threading.Tasks;
    using Web.Controllers;

    using static Common.WebConstants;

    [Area(Areas.Moderator)]
    [Authorize(Roles = Roles.TournamentModerator)]
    public class TournamentsController : Controller
    {
        private readonly IModeratorTournamentService moderatorTournaments;
        private readonly IGameService games;
        private readonly ITeamService teams;
        private readonly ITournamentService tournaments;

        public TournamentsController(
            IModeratorTournamentService moderatorTournaments,
            IGameService games,
            ITeamService teams,
            ITournamentService tournaments)
        {
            this.moderatorTournaments = moderatorTournaments;
            this.games = games;
            this.teams = teams;
            this.tournaments = tournaments;
        }

        public async Task<IActionResult> Create()
            => View(new CreateTournamentFormModel
            {
                Games = await this.games.AllToSelectListAsync(),
                StartDate = DateTime.UtcNow
            });

        [HttpPost]
        public async Task<IActionResult> Create(CreateTournamentFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Games = await this.games.AllToSelectListAsync();
                return View(model);
            }

            await this.moderatorTournaments.CreateAsync(
                model.Name,
                model.Prize,
                model.StartDate,
                model.GameId);

            TempData.AddSuccessMessage(string.Format(SuccessMessages.AddedTournament, model.Name));

            return this.RedirectToAction(
                nameof(Web.Controllers.TournamentsController.Index),
                "Tournaments",
                new { area = string.Empty });
        }

        public async Task<IActionResult> Manage(int id)
            => View(new ManageTournamentViewModel
            {
                Id = id,
                Teams = await this.teams.AllToSelectListAsync(id),
                Tournament = await this.moderatorTournaments.GetTournamentAsync(id)
            });

        public async Task<IActionResult> End(int id)
            => View(new EndTournamentViewModel
            {
                Id = id,
                Teams = await this.teams.AllToSelectListAsync(id),
            });

        [HttpPost]
        public async Task<IActionResult> End(EndTournamentViewModel model)
        {
            var tournamentId = model.Id;
            var teamId = model.Id;

            if (!await this.tournaments.ContainsAsync(tournamentId) ||
                !await this.teams.ContainsAsync(teamId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.moderatorTournaments.EndTournamentAndChooseAWinner(model.Id, model.TeamId);
            TempData.AddSuccessMessage(string.Format(SuccessMessages.TournamentEnded, model.Name));

            return RedirectToAction(
                nameof(HomeController.Index),
                "Home",
                new { area = string.Empty });
        }

        public async Task<IActionResult> Start(int id)
        {
            if (!await this.tournaments.ContainsAsync(id))
            {
                return NotFound();
            }

            if (this.tournaments.HasStarted(id) ||
                this.tournaments.HasEnded(id))
            {
                TempData.AddErrorMessage(ErrorMessages.TournamentStarted);
            }
            else
            {
                await this.moderatorTournaments.StartAsync(id);
                TempData.AddSuccessMessage(SuccessMessages.TournamentStarted);
            }

            return RedirectToAction(
                nameof(TournamentsController.Manage),
                "Tournaments",
                new { area = Areas.Moderator, id });
        }
    }
}

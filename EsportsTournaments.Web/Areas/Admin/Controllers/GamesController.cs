namespace EsportsTournaments.Web.Areas.Admin.Controllers
{
    using Core.Extensions;
    using Core.Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models.Games;
    using Services;
    using Services.Admin;
    using System.Threading.Tasks;

    using static Common.WebConstants;

    public class GamesController : BaseAdminController
    {
        private readonly IAdminGameService adminGames;
        private readonly IGameService games;

        public GamesController(IAdminGameService adminGames, IGameService games)
        {
            this.adminGames = adminGames;
            this.games = games;
        }

        public IActionResult Add() => this.View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Add(AddGameFormModel model)
        {
            if (await this.games.ContainsAsync(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name),
                    string.Format(ErrorMessages.GameExists, model.Name));
                return RedirectToAction(nameof(Add));
            }

            await this.adminGames.AddAsync(
                model.Name,
                model.Developer,
                model.GameImageUrl);

            TempData.AddSuccessMessage(string.Format(SuccessMessages.AddedGame, model.Name));

            return RedirectToAction(
                nameof(Web.Controllers.GamesController.Index),
                "Home", new { area = string.Empty });
        }
    }
}

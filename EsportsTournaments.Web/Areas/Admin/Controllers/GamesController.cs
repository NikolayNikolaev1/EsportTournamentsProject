namespace EsportsTournaments.Web.Areas.Admin.Controllers
{
    using Core.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models.Games;
    using Services.Admin;
    using System.Threading.Tasks;

    using static Common.WebConstants;

    public class GamesController : BaseAdminController
    {
        private readonly IAdminGameService games;

        public GamesController(IAdminGameService games)
        {
            this.games = games;
        }

        public IActionResult Add() => this.View();

        [HttpPost]
        public async Task<IActionResult> Add(AddGameFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await this.games.ContaintsAsync(model.Name))
            {
                ModelState.AddModelError(nameof(model.Name),
                    string.Format(ErrorMessages.GameExists, model.Name));
                return RedirectToAction(nameof(Add));
            }

            await this.games.AddAsync(
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

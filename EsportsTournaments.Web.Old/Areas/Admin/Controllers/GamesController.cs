namespace EsportsTournaments.Web.Areas.Admin.Controllers
{
    using Core.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Models.Games;
    using Services.Admin;
    using System.Threading.Tasks;
    using Web.Controllers;

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

            await this.games.AddAsync(
                model.Name,
                model.Developer,
                model.GameImageUrl);

            TempData.AddSuccessMessage($"Game {model.Name} added successfully!");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }
    }
}

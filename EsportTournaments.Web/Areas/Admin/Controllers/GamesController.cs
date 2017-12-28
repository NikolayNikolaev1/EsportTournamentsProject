using EsportTournaments.Core.Extensions;
using EsportTournaments.Services.Admin;
using EsportTournaments.Web.Areas.Admin.Models.Games;
using EsportTournaments.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EsportTournaments.Web.Areas.Admin.Controllers
{
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
                model.GameImageUrl,
                model.GameWebsite);

            TempData.AddSuccessMessage($"Game {model.Name} added successfully!");

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }
    }
}

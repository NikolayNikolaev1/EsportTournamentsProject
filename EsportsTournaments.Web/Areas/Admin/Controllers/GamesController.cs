namespace EsportsTournaments.Web.Areas.Admin.Controllers
{
    using Core.Extensions;
    using Core.Filters;
    using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        public GamesController(
            IAdminGameService adminGames,
            IGameService games,
            IWebHostEnvironment webHostEnvironment)
        {
            this.adminGames = adminGames;
            this.games = games;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Add() => this.View();

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Add(AddGameFormModel formModel)
        {
            if (await this.games.ContainsAsync(formModel.Name))
            {
                ModelState.AddModelError(nameof(formModel.Name),
                    string.Format(ErrorMessages.GameExists, formModel.Name));
                return RedirectToAction(nameof(Add));
            }

            var fileName = formModel.Image.FileName;
            var imageModelKey = nameof(formModel.Image);

            // Checks if image file extensions is valid.
            if (!ImageFileExtensions.FileExtensionValidation(formModel.Image.FileName))
            {
                ModelState.AddModelError(imageModelKey, ErrorMessages.InvalidImageFileExtension);
                return View(formModel);
            }

            // Checks if image file signature is valid
            if (!ImageFileExtensions.FileSignatureValidation(fileName, formModel.Image.OpenReadStream()))
            {
                ModelState.AddModelError(imageModelKey, ErrorMessages.InvalidImageFileSignature);
                return View(formModel);
            }

            // Uploads image to root folder and returns generated image file name.
            var imageName = await ImageFileExtensions.UploadFileAsync(formModel.Image, webHostEnvironment.WebRootPath);



            await this.adminGames.AddAsync(
                formModel.Name,
                formModel.Developer,
                imageName);

            TempData.AddSuccessMessage(string.Format(SuccessMessages.AddedGame, formModel.Name));

            return RedirectToAction(
                nameof(Web.Controllers.GamesController.Index),
                "Home", new { area = string.Empty });
        }
    }
}

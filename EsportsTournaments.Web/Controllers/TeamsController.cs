namespace EsportsTournaments.Web.Controllers
{
    using Core.Extensions;
    using Core.Filters;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Teams;
    using Services;
    using System.Threading.Tasks;

    using static Common.WebConstants;

    public class TeamsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly ITeamService teams;
        private readonly IGameService games;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TeamsController(
            ITeamService teams,
            IGameService games,
            UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment)
        {
            this.teams = teams;
            this.games = games;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public IActionResult Create() => View();

        [Authorize]
        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(CreateTeamViewModel formModel)
        {
            var userId = this.userManager.GetUserId(User);

            if (await this.teams.ContainsNameAsync(formModel.Name))
            {
                ModelState.AddModelError("Name",
                    string.Format(ErrorMessages.TeamNameExists, formModel.Name));
                return View(formModel);
            }

            if (await this.teams.ContainsTagAsync(formModel.Tag))
            {
                ModelState.AddModelError("Tag",
                    string.Format(ErrorMessages.TeamTagExists, formModel.Tag));
                return View(formModel);
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


            await this.teams.CreateAsync(
                    formModel.Name,
                    formModel.Tag,
                    imageName,
                    userId);

            TempData.AddSuccessMessage(string.Format(SuccessMessages.CreateTeam, formModel.Name));

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = string.Empty });
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await this.teams.ContainsAsync(id))
            {
                return NotFound();
            }

            var formModel = new TeamDetailsViewModel
            {
                Team = await this.teams.DetailsAsync(id)
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = this.userManager.GetUserId(User);
                formModel.UserIsInTeam = await this.teams.HasPlayerAsync(id, userId);
            }

            return View(formModel);
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(new TeamListingViewModel
            {
                Teams = await this.teams.AllAsync(page),
                TotalTeams = await this.teams.TotalAsync(),
                CurrentPage = page
            });

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            if (!await this.teams.ContainsAsync(id))
            {
                return NotFound();
            }

            var userId = this.userManager.GetUserId(User);

            if (await this.teams.HasPlayerAsync(id, userId))
            {
                return BadRequest();
            }

            await this.teams.JoinAsync(id, userId);

            TempData.AddSuccessMessage(SuccessMessages.TeamJoined);

            return RedirectToAction(nameof(Details), new { id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if (!await this.teams.ContainsAsync(id))
            {
                return NotFound();
            }

            var userId = this.userManager.GetUserId(User);

            if (!await this.teams.HasPlayerAsync(id, userId))
            {
                return BadRequest();
            }

            await this.teams.LeaveAsync(id, userId);

            TempData.AddSuccessMessage(SuccessMessages.TeamLeft);

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}

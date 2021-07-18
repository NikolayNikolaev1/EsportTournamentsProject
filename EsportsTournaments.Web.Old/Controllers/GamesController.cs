namespace EsportsTournaments.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models.Games;
    using Services;
    using System.Threading.Tasks;

    public class GamesController : Controller
    {
        public IGameService games;

        public GamesController(IGameService games)
        {
            this.games = games;
        }

        public async Task<IActionResult> Index(int page = 1)
            => View(new GamesListingViewModel
            {
                Games = await this.games.AllAsync(page),
                TotalTournaments = await this.games.TotalAsync(),
                CurrentPage = page
            });
    }
}

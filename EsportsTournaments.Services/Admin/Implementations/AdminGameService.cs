namespace EsportsTournaments.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminGameService : IAdminGameService
    {
        private readonly EsportsTournamentsDbContext db;

        public AdminGameService(EsportsTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> AddAsync(string name, string developer, string gameImageUrl)
        {
            if (this.db.Games.Any(g => g.Name == name))
            {
                return false;
            }

            var game = new Game
            {
                Name = name,
                Developer = developer,
                GameImageUrl = gameImageUrl
            };

            this.db.Add(game);
            await this.db.SaveChangesAsync();

            return true;
        }
    }
}

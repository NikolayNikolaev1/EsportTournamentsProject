using EsportTournaments.Data;
using EsportTournaments.Data.Models;
using System.Threading.Tasks;

namespace EsportTournaments.Services.Admin.Implementations
{
    public class AdminGameService : IAdminGameService
    {
        private readonly EsportTournamentsDbContext db;

        public AdminGameService(EsportTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(string name, string developer, string gameImageUrl, string gameWebsite)
        {
            var game = new Game
            {
                Name = name,
                Developer = developer,
                GameImageUrl = gameImageUrl,
                GameWebsite = gameWebsite
            };

            this.db.Add(game);
            await this.db.SaveChangesAsync();
        }
    }
}

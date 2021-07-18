using EsportsTournaments.Data;
using EsportsTournaments.Data.Models;
using System.Threading.Tasks;

namespace EsportsTournaments.Services.Admin.Implementations
{
    public class AdminGameService : IAdminGameService
    {
        private readonly EsportsTournamentsDbContext db;

        public AdminGameService(EsportsTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(string name, string developer, string gameImageUrl)
        {
            var game = new Game
            {
                Name = name,
                Developer = developer,
                GameImageUrl = gameImageUrl
            };

            this.db.Add(game);
            await this.db.SaveChangesAsync();
        }
    }
}

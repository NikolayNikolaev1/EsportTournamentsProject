namespace EsportsTournaments.Services.Admin.Implementations
{
    using Data;
    using Data.Models;
    using System.Threading.Tasks;

    public class AdminGameService : IAdminGameService
    {
        private readonly EsportsTournamentsDbContext db;

        public AdminGameService(EsportsTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(string name, string developer, string fileName)
        {
            var game = new Game
            {
                Name = name,
                Developer = developer,
                Image = fileName
            };

            this.db.Add(game);
            await this.db.SaveChangesAsync();
        }
    }
}

using AutoMapper.QueryableExtensions;
using EsportTournaments.Data;
using EsportTournaments.Data.Models;
using EsportTournaments.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportTournaments.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly EsportTournamentsDbContext db;

        public GameService(EsportTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<GameListingServiceModel>> AllAsync(int page = 1)
         => await this.db
                .Games
                .Skip((page - 1) * 6)
                .Take(6)
                .ProjectTo<GameListingServiceModel>()
                .ToListAsync();

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
            => await this.db
            .Games
            .ToListAsync();

        public async Task<int> TotalAsync()
        => await this.db
                .Games
                .CountAsync();
    }
}

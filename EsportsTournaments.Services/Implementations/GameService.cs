namespace EsportsTournaments.Services.Implementations
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GameService : IGameService
    {
        private readonly EsportsTournamentsDbContext db;
        private readonly IMapper mapper;

        public GameService(EsportsTournamentsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GameListingServiceModel>> AllAsync(int page = 1)
         => await this.mapper
            .ProjectTo<GameListingServiceModel>(
                this.db
                .Games
                .Skip((page - 1) * 6)
                .Take(6))
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

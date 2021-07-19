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

    public class UserService : IUserService
    {
        private readonly EsportsTournamentsDbContext db;
        private readonly IMapper mapper;

        public UserService(EsportsTournamentsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Team>> GetAllCreatedTeamsAsync(int gameId, string id)
            => await this.db
                    .Teams
                    .Where(t => t.CaptainId == id && t.GameId == gameId)
                    .ToListAsync();

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
            => await this.mapper
            .ProjectTo<UserProfileServiceModel>(
                this.db
                .Users
                .Where(u => u.Id == id))
            .FirstOrDefaultAsync();
    }
}

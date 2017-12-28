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
    public class UserService : IUserService
    {
        private readonly EsportTournamentsDbContext db;

        public UserService(EsportTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Team>> GetAllCreatedTeamsAsync(int gameId, string id)
            => await this.db
                    .Teams
                    .Where(t => t.CaptainId == id && t.GameId == gameId)
                    .ToListAsync();

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
            => await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserProfileServiceModel>(new
                {
                    UserId = id
                })
                .FirstOrDefaultAsync();
    }
}

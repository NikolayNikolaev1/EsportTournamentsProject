using AutoMapper.QueryableExtensions;
using EsportsTournaments.Data;
using EsportsTournaments.Data.Models;
using EsportsTournaments.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportsTournaments.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly EsportsTournamentsDbContext db;

        public UserService(EsportsTournamentsDbContext db)
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

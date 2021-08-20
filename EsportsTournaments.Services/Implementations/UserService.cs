namespace EsportsTournaments.Services.Implementations
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.Teams;
    using Models.Users;
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

        public async Task<IEnumerable<Team>> GetAllCreatedTeamsAsync(string id)
            => await this.db
                    .Teams
                    .Where(t => t.CaptainId == id)
                    .ToListAsync();

        // Returns team list model of all teams created by user with given id.
        public async Task<IEnumerable<TeamListingServiceModel>> GetAllCreatedTeamsListAsync(string id)
            => await this.db
            .Teams
            .Where(t => t.CaptainId == id)
            .ProjectTo<TeamListingServiceModel>(this.mapper.ConfigurationProvider)
            .ToListAsync();

        // Returns team list model of all teams joined by user with given id.
        public async Task<IEnumerable<TeamListingServiceModel>> GetAllJoinedTeamsListAsync(string id)
            => await this.db
            .Teams
            .Where(t => t.Players.Any(p => p.PlayerId == id))
            .ProjectTo<TeamListingServiceModel>(this.mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
            => await this.db
            .Users
            .Where(u => u.Id == id)
            .ProjectTo<UserProfileServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}

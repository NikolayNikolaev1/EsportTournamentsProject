namespace EsportsTournaments.Services
{
    using Data.Models;
    using Models.Teams;
    using Services.Models.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string username);

        Task<IEnumerable<Team>> GetAllCreatedTeamsAsync(string id);

        Task<IEnumerable<TeamListingServiceModel>> GetAllCreatedTeamsListAsync(string id);

        Task<IEnumerable<TeamListingServiceModel>> GetAllJoinedTeamsListAsync(string id);
    }
}

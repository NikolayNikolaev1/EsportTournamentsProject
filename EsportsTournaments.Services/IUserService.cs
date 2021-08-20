namespace EsportsTournaments.Services
{
    using Data.Models;
    using Services.Models.Users;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string username);

        Task<IEnumerable<Team>> GetAllCreatedTeamsAsync(string id);
    }
}

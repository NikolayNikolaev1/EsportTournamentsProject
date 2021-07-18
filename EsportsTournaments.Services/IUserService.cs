using EsportsTournaments.Data.Models;
using EsportsTournaments.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportsTournaments.Services
{
    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string username);

        Task<IEnumerable<Team>> GetAllCreatedTeamsAsync(int gameId, string id);
    }
}

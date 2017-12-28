using EsportTournaments.Data.Models;
using EsportTournaments.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportTournaments.Services
{
    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string username);

        Task<IEnumerable<Team>> GetAllCreatedTeamsAsync(int gameId, string id);
    }
}

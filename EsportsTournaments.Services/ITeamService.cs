using EsportsTournaments.Data.Models;
using EsportsTournaments.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportsTournaments.Services
{
    public interface ITeamService
    {
        Task CreateAsync(string name, string tag, string teamImageUrl, string captainId, string gameId);

        Task<IEnumerable<Game>> GetAllGames();

        Task<TeamDetailsServiceModel> ByIdAsync(int id);

        Task<bool> PlayerJoinAsync(int teamId, string userId);

        Task<bool> PlayerLeaveAsync(int teamId, string userId);

        Task<IEnumerable<TeamListingServiceModel>> AllAsync(int page = 1);

        Task<bool> UserIsInTeamAsync(int teamId, string userId);

        Task<int> TotalAsync();
    }
}

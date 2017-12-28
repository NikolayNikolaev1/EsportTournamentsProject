using EsportTournaments.Data.Models;
using EsportTournaments.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportTournaments.Services
{
    public interface ITeamService
    {
        Task CreateAsync(string name, string tag, string teamImageUrl, string captainId, string gameId);

        Task<IEnumerable<Game>> GetAllGames();

        Task<TeamDetailsServiceModel> ByIdAsync(int id);

        Task<bool> PlayerJoin(int teamId, string userId);

        Task<IEnumerable<TeamListingServiceModel>> AllAsync(int page = 1);

        Task<bool> PlayerLeave(int teamId, string userId);

        Task<bool> UserIsInTeam(int teamId, string userId);

        Task<int> TotalAsync();
    }
}

namespace EsportsTournaments.Services
{
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models.Teams;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeamService
    {
        Task<IEnumerable<TeamListingServiceModel>> AllAsync(int page = 1);

        Task<IEnumerable<SelectListItem>> AllToSelectListAsync(int tournamentId = 0);

        Task<bool> ContainsAsync(int id);

        Task<bool> ContainsNameAsync(string name);

        Task<bool> ContainsTagAsync(string tag);

        Task CreateAsync(string name, string tag, string teamImageUrl, string captainId, int gameId);

        Task<TeamDetailsServiceModel> DetailsAsync(int id);

        Task<bool> HasPlayerAsync(int teamId, string playerId);

        Task JoinAsync(int teamId, string playerId);

        Task LeaveAsync(int teamId, string playerId);

        Task<int> TotalAsync();
    }
}

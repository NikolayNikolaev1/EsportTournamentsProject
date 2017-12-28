using EsportTournaments.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportTournaments.Services
{
    public interface ITournamentService
    {
        Task<TournamentDetailsServiceModel> ById(int id);

        Task<IEnumerable<TournamentListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();

        Task<bool> TeamJoin(int tournamentId, int teamId);

        Task<bool> TeamLeave(int tournamentId, int teamId);

        Task<int> GetTournamentGameId(int id);
    }
}

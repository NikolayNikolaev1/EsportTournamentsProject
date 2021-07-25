namespace EsportsTournaments.Services
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITournamentService
    {
        Task<TournamentDetailsServiceModel> ById(int id);

        Task<IEnumerable<TournamentListingServiceModel>> AllAsync(int page = 1);

        Task<bool> ContainsAsync(int id);

        bool HasStarted(int id);

        bool HasEnded(int id);

        Task<int> TotalAsync();

        Task<bool> TeamJoin(int tournamentId, int teamId);

        Task<bool> TeamLeave(int tournamentId, int teamId);

        Task<int> GetTournamentGameId(int id);
    }
}

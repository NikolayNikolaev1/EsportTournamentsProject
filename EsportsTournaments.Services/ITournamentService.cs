namespace EsportsTournaments.Services
{
    using Models.Teams;
    using Models.Tournaments;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITournamentService
    {
        Task<IEnumerable<TournamentListingServiceModel>> AllAsync(int page = 1);

        Task<bool> ContainsAsync(int id);

        Task<TournamentDetailsServiceModel> DetailsAsync(int id);

        Task<IEnumerable<TeamListingServiceModel>> GetTeamsAsync(int id);

        bool HasEnded(int id);

        bool HasStarted(int id);

        Task<bool> TeamJoin(int tournamentId, int teamId);

        Task<bool> TeamLeave(int tournamentId, int teamId);

        Task<int> TotalAsync();

        Task<int> GetTournamentGameId(int id);
    }
}

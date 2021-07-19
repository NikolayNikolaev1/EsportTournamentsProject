namespace EsportsTournaments.Services.Moderator
{
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModeratorTournamentService
    {
        Task CreateAsync(string name, PrizeType prize, DateTime startDate, string gameId);

        Task<bool> StartAsync(int id);

        Task<IEnumerable<TeamInTournamentServiceModel>> GetTeamsInTournamentAsync(int id);

        Task<bool> EndTournamentAndChooseAWinner(int tournamentId, int teamId);

        Task<Tournament> GetTournamentAsync(int id);
    }
}

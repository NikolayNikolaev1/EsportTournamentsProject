namespace EsportsTournaments.Services.Moderator
{
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IModeratorTournamentService
    {
        Task CreateAsync(string name, PrizeType prize, DateTime startDate, int gameId);

        Task EndTournamentAndChooseAWinner(int tournamentId, int teamId);

        Task StartAsync(int id);

        Task<IEnumerable<TeamInTournamentServiceModel>> GetTeamsInTournamentAsync(int id);

        Task<Tournament> GetTournamentAsync(int id);
    }
}

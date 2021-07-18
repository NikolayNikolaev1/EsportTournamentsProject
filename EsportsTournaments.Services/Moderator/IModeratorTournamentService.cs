using EsportsTournaments.Data.Models;
using EsportsTournaments.Services.Moderator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportsTournaments.Services.Moderator
{
    public interface IModeratorTournamentService
    {
        Task CreateAsync(string name, PrizeType prize, DateTime startDate, string gameId);

        Task<bool> StartAsync(int id);

        Task<IEnumerable<TeamInTournamentServiceModel>> GetTeamsInTournamentAsync(int id);

        Task<bool> EndTournamentAndChooseAWinner(int tournamentId, int teamId);

        Task<Tournament> GetTournamentAsync(int id);
    }
}

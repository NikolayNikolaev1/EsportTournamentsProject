using EsportTournaments.Data.Models;
using EsportTournaments.Services.Moderator.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportTournaments.Services.Moderator
{
    public interface IModeratorTournamentService
    {
        Task CreateAsync(string name, PrizeType prize, DateTime startDate, string gameId);

        Task<bool> StartAsync(int id);

        Task<IEnumerable<TeamInTournamentServiceModel>> GetTeamsInTournamentAsync(int id);

        Task<bool> EndTournamentAndChooseAWinner(int tournamentId, int teamId);
    }
}

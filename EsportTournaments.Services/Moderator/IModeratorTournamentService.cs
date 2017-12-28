using EsportTournaments.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportTournaments.Services.Moderator
{
    public interface IModeratorTournamentService
    {
        Task CreateAsync(string name, PrizeType prize, DateTime startDate, string gameId);
    }
}

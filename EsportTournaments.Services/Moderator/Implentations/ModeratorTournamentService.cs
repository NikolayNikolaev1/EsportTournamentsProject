using EsportTournaments.Data;
using EsportTournaments.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EsportTournaments.Services.Moderator.Implentations
{
    public class ModeratorTournamentService : IModeratorTournamentService
    {
        private readonly EsportTournamentsDbContext db;

        public ModeratorTournamentService(EsportTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string name, PrizeType prize, DateTime startDate, string gameId)
        {
            if (startDate < DateTime.UtcNow)
            {
                //TODO: Add error 
            }

            var tournament = new Tournament
            {
                Name = name,
                Prize = prize,
                StartDate = startDate,
                HasEnded = false,
                GameId = int.Parse(gameId)
            };

            this.db.Add(tournament);

            await this.db.SaveChangesAsync();
        }

        public async Task<int> TotalAsync()
            => await this.db
                .Tournaments
                .CountAsync();
    }
}

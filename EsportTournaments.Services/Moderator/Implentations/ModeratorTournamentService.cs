using AutoMapper.QueryableExtensions;
using EsportTournaments.Data;
using EsportTournaments.Data.Models;
using EsportTournaments.Services.Moderator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            //if (startDate < DateTime.UtcNow)
            //{
            //    //TODO: Add error 
            //}

            var tournament = new Tournament
            {
                Name = name,
                Prize = prize,
                StartDate = startDate,
                HasStarted = false,
                HasEnded = false,
                GameId = int.Parse(gameId)
            };

            this.db.Add(tournament);

            await this.db.SaveChangesAsync();
        }

        public async Task<bool> EndTournamentAndChooseAWinner(int tournamentId, int teamId)
        {
            var tournament = this.db
                    .Tournaments
                    .Where(t => t.Id == tournamentId)
                    .FirstOrDefault();

            var team = this.db
                    .Teams
                    .Where(t => t.Id == teamId)
                    .FirstOrDefault();

            if (tournament == null
                || team == null)
            {
                return false;
            }

            tournament.HasStarted = false;
            tournament.HasEnded = true;
            team.TournamentsWon++;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TeamInTournamentServiceModel>> GetTeamsInTournamentAsync(int id)
            => await this.db
                .Tournaments
                .Where(t => t.Id == id)
                .SelectMany(t => t.Teams.Select(team => team.Team))
                .ProjectTo<TeamInTournamentServiceModel>()
                .ToListAsync();

        public async Task<bool> StartAsync(int id)
        {
            var currentTournament =  this.db
                    .Tournaments
                    .Where(t => t.Id == id)
                    .FirstOrDefault();

            //if (currentTournament.StartDate != DateTime.UtcNow)
            //{
            //    return false;
            //}

            if (currentTournament == null
                || currentTournament.HasStarted == true
                || currentTournament.HasEnded == true)
            {
                return false;
            }

            currentTournament.HasStarted = true;

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<int> TotalAsync()
            => await this.db
                .Tournaments
                .CountAsync();
    }
}

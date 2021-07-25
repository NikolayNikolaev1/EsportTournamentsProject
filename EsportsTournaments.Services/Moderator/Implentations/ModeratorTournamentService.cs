namespace EsportsTournaments.Services.Moderator.Implentations
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ModeratorTournamentService : IModeratorTournamentService
    {
        private readonly EsportsTournamentsDbContext db;
        private readonly IMapper mapper;

        public ModeratorTournamentService(EsportsTournamentsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CreateAsync(string name, PrizeType prize, DateTime startDate, int gameId)
        {
            var tournament = new Tournament
            {
                Name = name,
                Prize = prize,
                StartDate = startDate,
                HasStarted = false,
                HasEnded = false,
                GameId = gameId
            };

            await this.db.AddAsync(tournament);
            await this.db.SaveChangesAsync();
        }

        public async Task EndTournamentAndChooseAWinner(int tournamentId, int teamId)
        {
            var tournament = await this.db
                .Tournaments
                .FirstOrDefaultAsync(t => t.Id == tournamentId);

            var team = await this.db
                .Teams
                .FirstOrDefaultAsync(t => t.Id == teamId);

            tournament.HasStarted = false;
            tournament.HasEnded = true;
            team.TournamentsWon++;

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TeamInTournamentServiceModel>> GetTeamsInTournamentAsync(int id)
            => await this.mapper
            .ProjectTo<TeamInTournamentServiceModel>(
                this.db
                .Tournaments
                .Where(t => t.Id == id)
                .SelectMany(t => t.Teams.Select(team => team.Team)))
            .ToListAsync();

        public async Task<Tournament> GetTournamentAsync(int id)
            => await this.db
                .Tournaments
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

        public async Task StartAsync(int id)
        {
            var currentTournament = this.db
                    .Tournaments
                    .FirstOrDefault(t => t.Id == id);

            currentTournament.HasStarted = true;
            await this.db.SaveChangesAsync();
        }

        public async Task<int> TotalAsync()
            => await this.db
                .Tournaments
                .CountAsync();
    }
}

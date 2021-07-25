namespace EsportsTournaments.Services.Implementations
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TournamentService : ITournamentService
    {
        private readonly EsportsTournamentsDbContext db;
        private readonly IMapper mapper;

        public TournamentService(EsportsTournamentsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<TournamentDetailsServiceModel> ById(int id)
            => await this.mapper
            .ProjectTo<TournamentDetailsServiceModel>(
                this.db
                .Tournaments
                .Where(t => t.Id == id))
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<TournamentListingServiceModel>> AllAsync(int page = 1)
             => await this.mapper
            .ProjectTo<TournamentListingServiceModel>(
                 this.db
                 .Tournaments
                 .OrderBy(t => t.HasEnded)
                 .ThenBy(t => t.StartDate)
                 .Skip((page - 1) * 6)
                 .Take(6))
            .ToListAsync();

        public async Task<bool> ContainsAsync(int id)
            => await this.db
            .Tournaments
            .AnyAsync(t => t.Id == id);

        public bool HasStarted(int id)
            => this.db
            .Tournaments
            .FirstOrDefault(t => t.Id == id)
            .HasStarted;

        public bool HasEnded(int id)
            => this.db
            .Tournaments
            .FirstOrDefault(t => t.Id == id)
            .HasEnded;

        public async Task<int> TotalAsync()
            => await this.db
                .Tournaments
                .CountAsync();

        public async Task<bool> TeamJoin(int tournamentId, int teamId)
        {
            var tournamentInfo = await this.GetTournamentInfo(tournamentId, teamId);

            if (tournamentInfo.TeamIsInTournament
                || tournamentInfo == null)
            {
                //TODO ADD MESSAGE!!
                return false;
            }

            var teamInTournament = new TeamTournament
            {
                TeamId = teamId,
                TournamentId = tournamentId
            };

            this.db.Add(teamInTournament);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> TeamLeave(int tournamentId, int teamId)
        {
            var tournamentInfo = await this.GetTournamentInfo(tournamentId, teamId);

            if (tournamentInfo.TeamIsInTournament
                || tournamentInfo == null)
            {
                //TODO ADD MESSAGE!!
                return false;
            }

            var teamInTournament = await this.db
                    .FindAsync(typeof(TeamTournament), teamId, tournamentId);

            this.db.Remove(teamInTournament);

            return true;
        }

        public async Task<int> GetTournamentGameId(int id)
            => await this.db
                    .Tournaments
                    .Where(t => t.Id == id)
                    .Select(t => t.GameId)
                    .FirstOrDefaultAsync();

        private async Task<TournamentWithTeamInfo> GetTournamentInfo(int tournamentId, int teamId)
            => await this.db
                .Tournaments
                .Where(t => t.Id == tournamentId)
                .Select(t => new TournamentWithTeamInfo
                {
                    TeamIsInTournament = t.Teams.Any(team => team.TeamId == teamId)
                })
                .FirstOrDefaultAsync();
    }
}

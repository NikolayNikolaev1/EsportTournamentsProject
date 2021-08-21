namespace EsportsTournaments.Services.Implementations
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using EsportsTournaments.Services.Models.Teams;
    using Microsoft.EntityFrameworkCore;
    using Models.Tournaments;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.WebConstants;

    public class TournamentService : ITournamentService
    {
        private readonly EsportsTournamentsDbContext db;
        private readonly IMapper mapper;

        public TournamentService(EsportsTournamentsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TournamentListingServiceModel>> AllAsync(int page = 1)
             => await this.db
            .Tournaments
            .OrderBy(t => t.HasEnded)
            .ThenBy(t => t.StartDate)
            .Skip((page - 1) * PaginationSize)
            .Take(PaginationSize)
            .ProjectTo<TournamentListingServiceModel>(this.mapper.ConfigurationProvider)
            .ToListAsync();

        public async Task<bool> ContainsAsync(int id)
            => await this.db
            .Tournaments
            .AnyAsync(t => t.Id == id);

        public async Task<TournamentDetailsServiceModel> DetailsAsync(int id)
            => await this.db
            .Tournaments
            .ProjectTo<TournamentDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(t => t.Id == id);

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
            => await this.db.Tournaments
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

        public async Task<IEnumerable<TeamListingServiceModel>> GetTeamsAsync(int id)
            => await this.db
            .Teams
            .Where(t => t.Tournaments.Any(tr => tr.TournamentId == id))
            .ProjectTo<TeamListingServiceModel>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }
}

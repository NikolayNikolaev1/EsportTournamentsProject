using AutoMapper.QueryableExtensions;
using EsportTournaments.Data;
using EsportTournaments.Data.Models;
using EsportTournaments.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportTournaments.Services.Implementations
{
    public class TournamentService : ITournamentService
    {
        private readonly EsportTournamentsDbContext db;

        public TournamentService(EsportTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task<TournamentDetailsServiceModel> ById(int id)
            => await this.db
                .Tournaments
                .Where(t => t.Id == id)
                .ProjectTo<TournamentDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TournamentListingServiceModel>> AllAsync(int page = 1)
             => await this.db
                 .Tournaments
                 .Where(t => t.HasEnded == false)
                 .OrderBy(t => t.StartDate)
                 .Skip((page - 1) * 6)
                 .Take(6)
                 .ProjectTo<TournamentListingServiceModel>()
                 .ToListAsync();

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

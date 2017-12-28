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
    public class TeamService : ITeamService
    {
        private readonly EsportTournamentsDbContext db;

        public TeamService(EsportTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task<TeamDetailsServiceModel> ByIdAsync(int id)
            => await this.db
                .Teams
                .Where(t => t.Id == id)
                .ProjectTo<TeamDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<TeamListingServiceModel>> AllAsync(int page = 1)
            => await this.db
                .Teams
                .OrderByDescending(t => t.TournamentsWon)
                .Skip((page - 1) * 6)
                .Take(6)
                .ProjectTo<TeamListingServiceModel>()
                .ToListAsync();

        public async Task CreateAsync(string name, string tag, string teamImageUrl, string captainId, string gameId)
        {
            var team = new Team
            {
                Name = name,
                Tag = tag,
                TeamImageUrl = teamImageUrl,
                TournamentsWon = 0,
                CaptainId = captainId,
                GameId = int.Parse(gameId)
            };

            team.Players.Add(new PlayerTeam
            {
                PlayerId = captainId,
                TeamId = team.Id
            });

            this.db.Add(team);

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAllGames()
            => await this.db.Games.ToListAsync();

        public async Task<bool> PlayerJoin(int teamId, string userId)
        {
            var teamInfo = await this.GetTeamInfo(teamId, userId);

            if (teamInfo.UserIsInTeam
                || teamInfo == null)
            {
                return false;
            }

            var playerInTeam = new PlayerTeam
            {
                PlayerId = userId,
                TeamId = teamId
            };

            this.db.Add(playerInTeam);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PlayerLeave(int teamId, string userId)
        {
            var teamInfo = await this.GetTeamInfo(teamId, userId);
            var currentTeam = this.db
                    .Teams
                    .Where(t => t.Id == teamId)
                    .FirstOrDefault();

            if (!teamInfo.UserIsInTeam
                || teamInfo == null
                || userId == currentTeam.CaptainId)
            {
                return false;
            }

            var playerInTeam = await this.db
                .FindAsync(typeof(PlayerTeam), userId, teamId);

            this.db.Remove(playerInTeam);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UserIsInTeam(int teamId, string userId)
            => await this.db
                .Teams
                .AnyAsync(t => t.Id == teamId && t.Players.Any(p => p.PlayerId == userId));


        public async Task<int> TotalAsync()
            => await this.db
                .Teams
                .CountAsync();

        private async Task<TeamWithPlayerInfo> GetTeamInfo(int teamId, string userId)
            => await this.db
                .Teams
                .Where(t => t.Id == teamId)
                .Select(t => new TeamWithPlayerInfo
                {
                    UserIsInTeam = t.Players.Any(p => p.PlayerId == userId)
                })
                .FirstOrDefaultAsync();
    }
}

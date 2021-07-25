namespace EsportsTournaments.Services.Implementations
{
    using AutoMapper;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TeamService : ITeamService
    {
        private readonly EsportsTournamentsDbContext db;
        private readonly IMapper mapper;

        public TeamService(EsportsTournamentsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<SelectListItem>> AllToSelectListAsync(int tournamentId = 0)
            => await this.db
            .Teams
            .Where(t => t.Tournaments.Any(tr => tr.TournamentId == tournamentId))
            .Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = g.Id.ToString()
            })
            .ToListAsync();

        public async Task<bool> ContainsAsync(int id)
            => await this.db
            .Teams
            .AnyAsync(t => t.Id == id);

        public async Task<TeamDetailsServiceModel> ByIdAsync(int id)
            => await this.mapper
            .ProjectTo<TeamDetailsServiceModel>(
                this.db
                .Teams
                .Where(t => t.Id == id))
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<TeamListingServiceModel>> AllAsync(int page = 1)
            => await this.mapper
            .ProjectTo<TeamListingServiceModel>(
                this.db
                .Teams
                .OrderByDescending(t => t.TournamentsWon)
                .Skip((page - 1) * 6)
                .Take(6))
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

            team.Players.ToList().Add(new PlayerTeam
            {
                PlayerId = captainId,
                TeamId = team.Id
            });

            this.db.Add(team);

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAllGames()
            => await this.db.Games.ToListAsync();

        public async Task<bool> PlayerJoinAsync(int teamId, string userId)
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

        public async Task<bool> PlayerLeaveAsync(int teamId, string userId)
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

        public async Task<bool> UserIsInTeamAsync(int teamId, string userId)
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

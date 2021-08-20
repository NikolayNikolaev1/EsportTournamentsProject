namespace EsportsTournaments.Services.Implementations
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.Teams;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Common.WebConstants;

    public class TeamService : ITeamService
    {
        private readonly EsportsTournamentsDbContext db;
        private readonly IMapper mapper;

        public TeamService(EsportsTournamentsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TeamListingServiceModel>> AllAsync(int page = 1)
            => await this.db
            .Teams
            .OrderByDescending(t => t.TournamentsWon)
            .Skip((page - 1) * PaginationSize)
            .Take(PaginationSize)
            .ProjectTo<TeamListingServiceModel>(this.mapper.ConfigurationProvider)
            .ToListAsync();

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

        public async Task<bool> ContainsNameAsync(string name)
            => await this.db
            .Teams
            .AnyAsync(t => t.Name.ToLower() == name.ToLower());

        public async Task<bool> ContainsTagAsync(string tag)
            => await this.db
            .Teams
            .AnyAsync(t => t.Tag.ToLower() == tag.ToLower());

        public async Task CreateAsync(
            string name,
            string tag,
            string fileName,
            string captainId)
        {
            var team = new Team
            {
                Name = name,
                Tag = tag,
                Image = fileName,
                TournamentsWon = 0,
                CaptainId = captainId
            };

            team.Players.ToList().Add(new PlayerTeam
            {
                PlayerId = captainId,
                TeamId = team.Id
            });

            await this.db.AddAsync(team);
            await this.db.SaveChangesAsync();
        }

        public async Task<TeamDetailsServiceModel> DetailsAsync(int id)
            => await this.db
            .Teams
            .ProjectTo<TeamDetailsServiceModel>(this.mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(t => t.Id == id);

        public async Task<bool> HasPlayerAsync(int teamId, string playerId)
            => await this.db
            .Teams
            .AnyAsync(t => t.Id == teamId
                && t.Players.Any(p => p.PlayerId == playerId));

        public async Task JoinAsync(int teamId, string playerId)
        {
            var playerInTeam = new PlayerTeam
            {
                PlayerId = playerId,
                TeamId = teamId
            };

            await this.db.AddAsync(playerInTeam);
            await this.db.SaveChangesAsync();
        }

        public async Task LeaveAsync(int teamId, string playerId)
        {
            var currentTeam = await this.db
                    .Teams
                    .FirstOrDefaultAsync(t => t.Id == teamId);

            if (playerId == currentTeam.CaptainId)
            {
                // Captain should not be able to leave team, with current logic. Needs refactoring.
                return;
            }

            var playerInTeam = await this.db
                .FindAsync(typeof(PlayerTeam), playerId, teamId);

            this.db.Remove(playerInTeam);
            await this.db.SaveChangesAsync();
        }

        public async Task<int> TotalAsync()
            => await this.db
            .Teams
            .CountAsync();
    }
}

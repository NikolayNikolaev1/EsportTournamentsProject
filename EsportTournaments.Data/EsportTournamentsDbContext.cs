using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EsportTournaments.Data.Models;

namespace EsportTournaments.Data
{
    public class EsportTournamentsDbContext : IdentityDbContext<User>
    {
        public EsportTournamentsDbContext(DbContextOptions<EsportTournamentsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<PlayerTeam>()
                .HasKey(pt => new { pt.PlayerId, pt.TeamId });

            builder
                .Entity<PlayerTeam>()
                .HasOne(t => t.Player)
                .WithMany(p => p.Teams)
                .HasForeignKey(t => t.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<PlayerTeam>()
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<TeamTournament>()
                .HasKey(tg => new { tg.TeamId, tg.TournamentId });

            builder
                .Entity<TeamTournament>()
                .HasOne(t => t.Tournament)
                .WithMany(tour => tour.Teams)
                .HasForeignKey(t => t.TournamentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<TeamTournament>()
                .HasOne(tour => tour.Team)
                .WithMany(g => g.Tournaments)
                .HasForeignKey(tour => tour.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Team>()
                .HasOne(t => t.Captain)
                .WithMany(c => c.CaptainOfTeams)
                .HasForeignKey(t => t.CaptainId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Team>()
                .HasOne(t => t.Game)
                .WithMany(g => g.Teams)
                .HasForeignKey(t => t.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Tournament>()
                .HasOne(t => t.Game)
                .WithMany(g => g.Tournaments)
                .HasForeignKey(t => t.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}

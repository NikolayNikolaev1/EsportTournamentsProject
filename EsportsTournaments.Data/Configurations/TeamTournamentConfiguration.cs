namespace EsportsTournaments.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    class TeamTournamentConfiguration : IEntityTypeConfiguration<TeamTournament>
    {
        public void Configure(EntityTypeBuilder<TeamTournament> teamTournament)
        {
            teamTournament
                .HasKey(tg => new { tg.TeamId, tg.TournamentId });

            teamTournament
                .HasOne(t => t.Tournament)
                .WithMany(tour => tour.Teams)
                .HasForeignKey(t => t.TournamentId)
                .OnDelete(DeleteBehavior.Restrict);

            teamTournament
                .HasOne(tour => tour.Team)
                .WithMany(g => g.Tournaments)
                .HasForeignKey(tour => tour.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

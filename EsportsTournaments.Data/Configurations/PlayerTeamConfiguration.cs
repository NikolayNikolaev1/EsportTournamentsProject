namespace EsportsTournaments.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class PlayerTeamConfiguration : IEntityTypeConfiguration<PlayerTeam>
    {
        public void Configure(EntityTypeBuilder<PlayerTeam> playerTeam)
        {
            playerTeam
                .HasKey(pt => new { pt.PlayerId, pt.TeamId });

            playerTeam
                .HasOne(t => t.Player)
                .WithMany(p => p.Teams)
                .HasForeignKey(t => t.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            playerTeam
                .HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

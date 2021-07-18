namespace EsportsTournaments.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> game)
        {
            game
                .HasMany(g => g.Teams)
                .WithOne(t => t.Game)
                .HasForeignKey(t => t.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            game
                .HasMany(g => g.Tournaments)
                .WithOne(t => t.Game)
                .HasForeignKey(t => t.GameId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

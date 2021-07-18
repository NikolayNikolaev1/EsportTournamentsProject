namespace EsportsTournaments.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user
                .HasMany(u => u.TeamsLeading)
                .WithOne(t => t.Captain)
                .HasForeignKey(t => t.CaptainId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

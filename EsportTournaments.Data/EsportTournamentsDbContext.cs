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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

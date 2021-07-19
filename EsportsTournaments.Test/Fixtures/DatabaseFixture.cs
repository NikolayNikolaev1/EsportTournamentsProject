namespace EsportsTournaments.Test.Fixtures
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class DatabaseFixture
    {
        public DatabaseFixture()
        {
            this.InitializeDatabase();
        }

        public EsportsTournamentsDbContext Context { get; private set; }

        private void InitializeDatabase()
            => this.Context = new EsportsTournamentsDbContext(
                new DbContextOptionsBuilder<EsportsTournamentsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
    }
}

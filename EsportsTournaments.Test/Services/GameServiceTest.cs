namespace EsportsTournaments.Test.Services
{
    using EsportsTournaments.Services.Implementations;
    using EsportsTournaments.Test.Fixtures;
    using System.Threading.Tasks;
    using Xunit;

    public class GameServiceTest : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture dbFixture;
        private readonly MapperFixture mapperFixture;

        public GameServiceTest(DatabaseFixture dbFixture, MapperFixture mapperFixture)
        {
            this.dbFixture = dbFixture;
        }

        [Fact]
        public async Task AllToSelectListAsyncShouldReturnAllGamesToSelectListItems()
        {
            // Arrange
            var gameService = new GameService(this.dbFixture.Context, mapperFixture.Mapper);

            // Act

            // Assert

        }
    }
}

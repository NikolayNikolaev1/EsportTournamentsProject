namespace EsportsTournaments.Test.Services
{
    using Data.Models;
    using EsportsTournaments.Services.Implementations;
    using FluentAssertions;
    using System.Threading.Tasks;
    using Xunit;

    public class GameServiceTest
    { 
        [Fact]
        public async Task AllToSelectListAsyncShouldReturnAllGamesToSelectListItems()
        {
            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext
                .AddRangeAsync(
                new Game { Id = 1, Name = "Alpha" },
                new Game { Id = 2, Name = "Beta" },
                new Game { Id = 3, Name = "Gamma" });
            await dbContext.SaveChangesAsync();

            var gameService = new GameService(dbContext, mapper);

            // Act
            var result = await gameService.AllToSelectListAsync();

            // Assert
            result
                .Should()
                .Contain(r => r.Value == "1" && r.Text == "Alpha")
                .And
                .Contain(r => r.Value == "2" && r.Text == "Beta")
                .And
                .Contain(r => r.Value == "3" && r.Text == "Gamma")
                .And
                .HaveCount(3);
        }
    }
}

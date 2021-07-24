namespace EsportsTournaments.Test.Services.Admin
{
    using Data.Models;
    using EsportsTournaments.Services.Admin.Implementations;
    using FluentAssertions;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AdminGameServiceTest
    {
        [Fact]
        public async Task AddAsyncShouldAddGameSuccessfully()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            var adminUserService = new AdminGameService(dbContext);

            // Act
            await adminUserService.AddAsync("TestName", "TestDeveloper", "http://www.testimage.test");

            // Assert
            dbContext
                .Games
                .Should()
                .Match(r => r.Any(
                    g => g.Name == "TestName" &&
                        g.Developer == "TestDeveloper" &&
                        g.GameImageUrl == "http://www.testimage.test"));
        }

        [Fact]
        public async Task ContainsAsyncShouldReturnFalse()
        {
            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var adminUserService = new AdminGameService(dbContext);

            // Act
            var result = await adminUserService.ContaintsAsync("TestName");

            // Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task ContainsAsyncShouldReturnTrue()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            await dbContext.Games.AddRangeAsync(new Game { Name = "TestName" });
            await dbContext.SaveChangesAsync();

            var adminUserService = new AdminGameService(dbContext);

            // Act
            var result = await adminUserService.ContaintsAsync("TestName");

            // Assert
            result
                .Should()
                .BeTrue();
        }
    }
}

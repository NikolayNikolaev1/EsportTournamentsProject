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
                .Contain(
                g => g.Name == "TestName" &&
                g.Developer == "TestDeveloper" &&
                g.Image == "http://www.testimage.test");
        }
    }
}

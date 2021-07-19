namespace EsportsTournaments.Test.Services.Admin
{
    using Data.Models;
    using EsportsTournaments.Services.Admin.Implementations;
    using Fixtures;
    using FluentAssertions;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AdminGameServiceTest
    {
        private readonly DatabaseFixture dbFixture;

        public AdminGameServiceTest(DatabaseFixture dbFixture)
        {
            this.dbFixture = dbFixture;
        }
        [Fact]
        public async Task AddSyncShouldAddGameSuccessfully()
        {
            // Arange
            var adminUserService = new AdminGameService(this.dbFixture.Context);

            // Act
            var result = await adminUserService.AddAsync("TestName", "TestDeveloper", "http://www.testimage.test");

            // Assert
            this.dbFixture
                .Context
                .Games
                .Should()
                .Match(r => r.Any(
                    g => g.Name == "TestName" &&
                        g.Developer == "TestDeveloper" &&
                        g.GameImageUrl == "http://www.testimage.test"));
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task AddAsyncShouldNotAddGameWithSameName()
        {
            // Arange
            this.dbFixture
                .Context
                .Games
                .Add(new Game { Id = 1, Name = "TestName" });

            var adminUserService = new AdminGameService(this.dbFixture.Context);

            // Act
            var result = await adminUserService.AddAsync(
                "TestName", "TestDeveloper", "http://www.testimage.test");

            // Assert
            result
                .Should()
                .BeFalse();

        }
    }
}

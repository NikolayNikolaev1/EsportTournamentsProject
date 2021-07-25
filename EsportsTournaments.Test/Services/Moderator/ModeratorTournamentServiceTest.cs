namespace EsportsTournaments.Test.Services.Moderator
{
    using Data.Models;
    using EsportsTournaments.Services.Moderator.Implentations;
    using FluentAssertions;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class ModeratorTournamentServiceTest
    {
        [Fact]
        public async Task CreateAsyncShouldSuccessfullyCreateTournament()
        {
            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            var moderatorTournamentService = new ModeratorTournamentService(dbContext, mapper);

            // Act
            var startDate = DateTime.Now.AddDays(1);
            await moderatorTournamentService.CreateAsync(
                "TestTournament", PrizeType.Money, startDate, 1);

            // Assert
            dbContext
                .Tournaments
                .Should()
                .Contain(g => g.Name == "TestTournament"
                    && g.Prize == PrizeType.Money
                    && g.StartDate == startDate
                    && g.GameId == 1);
        }

        [Fact]
        public async Task StartAsyncShouldSuccessfullyStartTournament()
        {
            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.AddAsync(new Tournament { Id = 1, HasStarted = false });
            await dbContext.SaveChangesAsync();

            var moderatorTournamentService = new ModeratorTournamentService(dbContext, mapper);

            // Act
            await moderatorTournamentService.StartAsync(1);

            // Assert
            dbContext
                .Tournaments
                .Should()
                .Contain(t => t.Id == 1 && t.HasStarted == true);
        }
    }
}

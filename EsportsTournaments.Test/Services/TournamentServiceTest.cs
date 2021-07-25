namespace EsportsTournaments.Test.Services
{
    using Data.Models;
    using EsportsTournaments.Services.Implementations;
    using FluentAssertions;
    using System.Threading.Tasks;
    using Xunit;

    public class TournamentServiceTest
    {
        [Fact]
        public async Task ContainsAsyncShouldReturnFalseIfGameDoesNotExist()
        {
            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            var tournamentService = new TournamentService(dbContext, mapper);

            // Act
            var result = await tournamentService.ContainsAsync(1);

            // Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task ContainsAsyncShouldReturnTrueIfTournamentExists()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.Tournaments.AddAsync(new Tournament { Id = 1 });
            await dbContext.SaveChangesAsync();

            var tournamentService = new TournamentService(dbContext, mapper);

            // Act
            var result = await tournamentService.ContainsAsync(1);

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task HasStartedShouldreturnTrueIfTournamentHasStarted()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.Tournaments.AddAsync(new Tournament { Id = 1, HasStarted = true });
            await dbContext.SaveChangesAsync();

            var tournamentService = new TournamentService(dbContext, mapper);

            // Act
            var result = tournamentService.HasStarted(1);

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task HasStartedShouldReturnFalseIfTournamentHasNotStarted()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.Tournaments.AddAsync(new Tournament { Id = 1, HasStarted = false });
            await dbContext.SaveChangesAsync();

            var tournamentService = new TournamentService(dbContext, mapper);

            // Act
            var result = tournamentService.HasStarted(1);

            // Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task HasEndedShouldReturnTrueIfTournamentHasEnded()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.Tournaments.AddAsync(new Tournament { Id = 1, HasEnded = true });
            await dbContext.SaveChangesAsync();

            var tournamentService = new TournamentService(dbContext, mapper);

            // Act
            var result = tournamentService.HasEnded(1);

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task HasEndedShouldReturnFalseIfTournamentHasNotEnded()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.Tournaments.AddAsync(new Tournament { Id = 1, HasEnded = false });
            await dbContext.SaveChangesAsync();

            var tournamentService = new TournamentService(dbContext, mapper);

            // Act
            var result = tournamentService.HasEnded(1);

            // Assert
            result
                .Should()
                .BeFalse();
        }
    }
}

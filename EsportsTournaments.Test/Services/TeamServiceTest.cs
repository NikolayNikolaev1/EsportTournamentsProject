namespace EsportsTournaments.Test.Services
{
    using Data.Models;
    using EsportsTournaments.Services.Implementations;
    using FluentAssertions;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;
    public class TeamServiceTest
    {
        [Fact]
        public async Task AllToSelectListAsyncShouldReturnAllTeamsInTournamentToSelectListItems()
        {

            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext
                .AddRangeAsync(
                new Team
                {
                    Id = 1,
                    Name = "Alpha",
                    Tournaments = new List<TeamTournament>
                    {
                        new TeamTournament { TeamId = 1, TournamentId = 1 }
                    }
                },
                new Team { Id = 2, Name = "Beta" },
                new Team 
                { 
                    Id = 3,
                    Name = "Gamma",
                    Tournaments = new List<TeamTournament>
                    {
                        new TeamTournament { TeamId = 3, TournamentId = 1 }
                    }
                });
            await dbContext.SaveChangesAsync();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.AllToSelectListAsync(1);

            // Assert
            result
                .Should()
                .Contain(r => r.Value == "1" && r.Text == "Alpha")
                .And
                .Contain(r => r.Value == "3" && r.Text == "Gamma")
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task ContainsAsyncShouldReturnFalseIfTeamDoesNotExist()
        {
            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.ContainsAsync(1);

            // Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task ContainsAsyncShouldReturnTrueIfTeamExist()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.Teams.AddAsync(new Team { Id = 1 });
            await dbContext.SaveChangesAsync();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.ContainsAsync(1);

            // Assert
            result
                .Should()
                .BeTrue();
        }
    }
}

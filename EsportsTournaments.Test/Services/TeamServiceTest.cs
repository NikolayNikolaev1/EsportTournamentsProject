namespace EsportsTournaments.Test.Services
{
    using Data.Models;
    using EsportsTournaments.Services.Implementations;
    using FluentAssertions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    public class TeamServiceTest
    {
        [Fact]
        public async Task AllAsyncShouldReturnAllTeamsOrderedByTournamentsWonDescending()
        {
            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.AddAsync(new Game { Id = 1, Name = "TestGame" });

            await dbContext
                .AddRangeAsync(
                new Team
                {
                    Id = 1,
                    Name = "Alpha",
                    Image = "test",
                    GameId = 1,
                    TournamentsWon = 0
                },
                new Team
                {
                    Id = 2,
                    Name = "Beta",
                    Image = "test",
                    GameId = 1,
                    TournamentsWon = 5
                },
                new Team
                {
                    Id = 3,
                    Name = "Gamma",
                    Image = "test",
                    GameId = 1,
                    TournamentsWon = 2
                });

            await dbContext.SaveChangesAsync();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.AllAsync();

            // Assert
            result
                .Should()
                .Match(t => t.ElementAt(0).Name == "Beta"
                    && t.ElementAt(1).Name == "Gamma"
                    && t.ElementAt(2).Name == "Alpha")
                .And
                .HaveCount(3);
        }

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

        [Fact]
        public async Task ContainsNameAsyncShouldReturnFalseIfTeamDoesNotExist()
        {
            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.ContainsNameAsync("Test");

            // Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task ContainsNameAsyncShouldReturnTrueIfTeamExist()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.Teams.AddAsync(new Team { Name = "Test" });
            await dbContext.SaveChangesAsync();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.ContainsNameAsync("Test");

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task ContainsTagAsyncShouldReturnFalseIfTeamDoesNotExist()
        {
            // Arrange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.ContainsTagAsync("Test");

            // Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task ContainsTagAsyncShouldReturnTrueIfTeamExist()
        {
            // Arange
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.Teams.AddAsync(new Team { Tag = "Test" });
            await dbContext.SaveChangesAsync();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.ContainsTagAsync("Test");

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [Fact]
        public async Task CreateAsyncShouldSuccessfullyCreateTeam()
        {
            // Arrage
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            await teamService.CreateAsync("TestName", "TEST", "http://testimage.test", "1", 1);

            // Assert
            dbContext
                .Teams
                .Should()
                .Contain(t => t.Name == "TestName"
                    && t.Tag == "TEST"
                    && t.Image == "http://testimage.test"
                    && t.CaptainId == "1"
                    && t.GameId == 1);
        }

        [Fact]
        public async Task DetailsAsyncShouldReturnTeamDetailsModel()
        {
            // Arrage
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext
                .AddAsync(new Game { Id = 1, Name = "GameTest" });

            await dbContext
                .AddAsync(new User { Id = "1", UserName = "Tester" });

            await dbContext
                .AddRangeAsync(
                new Team
                {
                    Id = 1,
                    Name = "Team Test",
                    Image = "testimage.test",
                    Tag = "Test",
                    GameId = 1,
                    CaptainId = "1",
                    Players = new List<PlayerTeam>
                    {
                        new PlayerTeam
                        {
                            PlayerId = "1",
                            TeamId = 1
                        }
                    },
                    TournamentsWon = 0
                });

            await dbContext.SaveChangesAsync();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.DetailsAsync(1);

            // Assert
            result
                .Id
                .Should()
                .Be(1);
            result
                .Name
                .Should()
                .Be("Team Test");
            result
                .Tag
                .Should()
                .Be("Test");
            result
                .TeamImageUrl
                .Should()
                .Be("testimage.test");
            result
                .Game
                .Should()
                .Be("GameTest");
            result.Captain
                .Should()
                .Be("Tester");
            result
                .Players
                .Should()
                .Contain(p => p.Equals("Tester"));

        }

        [Fact]
        public async Task TotalAsyncShouldReturnTeamsCount()
        {
            // Arrage
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext.AddRangeAsync(
                new Team { Id = 1 },
                new Team { Id = 2 },
                new Team { Id = 3 });

            await dbContext.SaveChangesAsync();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.TotalAsync();

            // Assert
            result
                .Should()
                .Be(3);
        }

        [Fact]
        public async Task HasPlayerAsyncShouldReturnFalseWhenTeamDoesNotHavePlayer()
        {
            // Arrage
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext
                .AddRangeAsync(
                new Team
                {
                    Id = 1,
                    Players = new List<PlayerTeam>
                    {
                        new PlayerTeam

                        {
                            PlayerId = "2",
                            TeamId = 1
                        }
                    }
                });

            await dbContext.SaveChangesAsync();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.HasPlayerAsync(1, "1");

            // Assert
            result
                .Should()
                .BeFalse();
        }

        [Fact]
        public async Task HasPlayerAsyncShouldReturnTrueIfTeamHasPlayer()
        {
            // Arrage
            var dbContext = Testing.CreateDatabaseContext();
            var mapper = Testing.CreateMapper();

            await dbContext
                .AddRangeAsync(
                new Team
                {
                    Id = 1,
                    Players = new List<PlayerTeam>
                    {
                        new PlayerTeam

                        {
                            PlayerId = "1",
                            TeamId = 1
                        }
                    }
                });

            await dbContext.SaveChangesAsync();

            var teamService = new TeamService(dbContext, mapper);

            // Act
            var result = await teamService.HasPlayerAsync(1, "1");

            // Assert
            result
                .Should()
                .BeTrue();
        }
    }
}

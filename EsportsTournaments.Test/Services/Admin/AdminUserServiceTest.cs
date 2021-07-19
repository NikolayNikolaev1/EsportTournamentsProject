namespace EsportsTournaments.Test.Services.Admin
{
    using AutoMapper;
    using Core.Mapping;
    using Data;
    using Data.Models;
    using EsportsTournaments.Services.Admin.Implementations;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AdminUserServiceTest
    {
        [Fact]
        public async Task AllAsyncShouldReturnAllUsers()
        {
            // Arange
            var dbOptions = new DbContextOptionsBuilder<EsportsTournamentsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var dbContext = new EsportsTournamentsDbContext(dbOptions);

            await dbContext
                .Users
                .AddRangeAsync(new User { Id = "1" }, new User { Id = "2" }, new User { Id = "3" });
            await dbContext.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>())
                .CreateMapper();

            var adminUserService = new AdminUserService(dbContext, mapper);
            // Act

            var result = await adminUserService.AllAsync();

            // Assert
            result
                .Should()
                .Match(r => r.ElementAt(0).Id == "1"
                    && r.ElementAt(1).Id == "2"
                    && r.ElementAt(2).Id == "3")
                .And
                .HaveCount(3);
        }
    }
}

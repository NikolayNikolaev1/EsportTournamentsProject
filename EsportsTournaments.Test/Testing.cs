namespace EsportsTournaments.Test
{
    using AutoMapper;
    using Data;
    using Core.Mapping;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class Testing
    {
        public static EsportsTournamentsDbContext CreateDatabaseContext()
            => new EsportsTournamentsDbContext(
                new DbContextOptionsBuilder<EsportsTournamentsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        public static IMapper CreateMapper()
            => new MapperConfiguration(
                cfg => cfg.AddProfile<AutoMapperProfile>())
                .CreateMapper();
    }
}

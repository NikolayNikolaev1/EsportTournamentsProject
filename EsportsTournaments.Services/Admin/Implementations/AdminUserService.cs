namespace EsportsTournaments.Services.Admin.Implementations
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private readonly EsportsTournamentsDbContext db;
        private readonly IMapper mapper;

        public AdminUserService(EsportsTournamentsDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
            .Users
            .ProjectTo<AdminUserListingServiceModel>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }
}

namespace EsportsTournaments.Services.Admin.Implementations
{
    using AutoMapper;
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
            => await this.mapper
                .ProjectTo<AdminUserListingServiceModel>(this.db.Users)
                .ToListAsync();
    }
}

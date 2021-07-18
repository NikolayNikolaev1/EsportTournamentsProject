using AutoMapper.QueryableExtensions;
using EsportsTournaments.Data;
using EsportsTournaments.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportsTournaments.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly EsportsTournamentsDbContext db;

        public AdminUserService(EsportsTournamentsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}

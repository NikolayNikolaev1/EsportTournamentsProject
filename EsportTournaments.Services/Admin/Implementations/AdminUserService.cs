using AutoMapper.QueryableExtensions;
using EsportTournaments.Data;
using EsportTournaments.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportTournaments.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly EsportTournamentsDbContext db;

        public AdminUserService(EsportTournamentsDbContext db)
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

using EsportTournaments.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportTournaments.Services.Admin
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}

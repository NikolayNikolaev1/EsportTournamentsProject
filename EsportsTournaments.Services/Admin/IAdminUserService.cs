using EsportsTournaments.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportsTournaments.Services.Admin
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}

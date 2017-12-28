using System.Threading.Tasks;

namespace EsportTournaments.Services.Admin
{
    public interface IAdminGameService
    {
        Task AddAsync(string name, string developer, string gameImageUrl, string gameWebsite);
    }
}

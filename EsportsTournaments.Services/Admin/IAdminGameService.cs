using System.Threading.Tasks;

namespace EsportsTournaments.Services.Admin
{
    public interface IAdminGameService
    {
        Task AddAsync(string name, string developer, string gameImageUrl);
    }
}

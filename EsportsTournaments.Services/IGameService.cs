using EsportsTournaments.Data.Models;
using EsportsTournaments.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportsTournaments.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();

        Task<IEnumerable<GameListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();
    }
}

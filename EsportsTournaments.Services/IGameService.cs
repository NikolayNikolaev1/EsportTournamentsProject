using EsportTournaments.Data.Models;
using EsportTournaments.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportTournaments.Services
{
    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();

        Task<IEnumerable<GameListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();
    }
}

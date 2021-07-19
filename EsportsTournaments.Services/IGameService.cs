namespace EsportsTournaments.Services
{
    using Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGameService
    {
        Task<IEnumerable<Game>> GetAllGamesAsync();

        Task<IEnumerable<GameListingServiceModel>> AllAsync(int page = 1);

        Task<int> TotalAsync();
    }
}

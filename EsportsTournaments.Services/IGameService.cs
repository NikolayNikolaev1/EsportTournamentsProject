namespace EsportsTournaments.Services
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGameService
    {
        Task<IEnumerable<SelectListItem>> AllToSelectListAsync();

        Task<IEnumerable<GameListingServiceModel>> AllAsync(int page = 1);

        Task<bool> ContainsAsync(string name);

        Task<int> TotalAsync();
    }
}

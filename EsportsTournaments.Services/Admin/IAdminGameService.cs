namespace EsportsTournaments.Services.Admin
{
    using System.Threading.Tasks;

    public interface IAdminGameService
    {
        Task<bool> AddAsync(string name, string developer, string gameImageUrl);
    }
}

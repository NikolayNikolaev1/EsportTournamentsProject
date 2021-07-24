namespace EsportsTournaments.Services.Admin
{
    using System.Threading.Tasks;

    public interface IAdminGameService
    {
        Task AddAsync(string name, string developer, string gameImageUrl);

        Task<bool> ContaintsAsync(string name);
    }
}

using EsportTournaments.Core.Mapping;
using EsportTournaments.Data.Models;

namespace EsportTournaments.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string LeagueOfLegendsAccount { get; set; }

        public string SteamAccount { get; set; }

        public string BlizzardAccount { get; set; }
    }
}

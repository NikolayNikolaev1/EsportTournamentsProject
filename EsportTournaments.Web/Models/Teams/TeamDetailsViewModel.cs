using EsportTournaments.Services.Models;

namespace EsportTournaments.Web.Models.Teams
{
    public class TeamDetailsViewModel
    {
        public TeamDetailsServiceModel Team { get; set; }

        public bool UserIsInTeam { get; set; }
    }
}

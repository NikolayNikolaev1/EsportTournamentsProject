namespace EsportsTournaments.Web.Models.Teams
{
    using Services.Models;

    public class TeamDetailsViewModel
    {
        public TeamDetailsServiceModel Team { get; set; }

        public bool UserIsInTeam { get; set; }
    }
}

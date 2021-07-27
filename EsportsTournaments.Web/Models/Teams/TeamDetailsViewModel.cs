namespace EsportsTournaments.Web.Models.Teams
{
    using Services.Models.Teams;

    public class TeamDetailsViewModel
    {
        public TeamDetailsServiceModel Team { get; set; }

        public bool UserIsInTeam { get; set; }
    }
}

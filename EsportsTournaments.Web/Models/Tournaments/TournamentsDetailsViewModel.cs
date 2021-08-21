namespace EsportsTournaments.Web.Models.Tournaments
{
    using Services.Models.Teams;
    using Services.Models.Tournaments;
    using System.Collections.Generic;

    public class TournamentsDetailsViewModel
    {
        public TournamentDetailsServiceModel Tournament { get; set; }

        public IEnumerable<TeamListingServiceModel> Teams { get; set; }

        public bool TeamIsInTournament { get; set; }
    }
}

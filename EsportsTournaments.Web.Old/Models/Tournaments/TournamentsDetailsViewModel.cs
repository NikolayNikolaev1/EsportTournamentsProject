namespace EsportsTournaments.Web.Models.Tournaments
{
    using Services.Models;

    public class TournamentsDetailsViewModel
    {
        public TournamentDetailsServiceModel Tournament { get; set; }

        public bool TeamIsInTournament { get; set; }
    }
}

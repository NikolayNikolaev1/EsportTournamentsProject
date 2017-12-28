using EsportTournaments.Services.Models;

namespace EsportTournaments.Web.Models.Tournaments
{
    public class TournamentsDetailsViewModel
    {
        public TournamentDetailsServiceModel Tournament { get; set; }

        public bool TeamIsInTournament { get; set; }
    }
}

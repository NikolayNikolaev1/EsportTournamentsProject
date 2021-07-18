using System;

namespace EsportTournaments.Services.Models
{
    public class TournamentWithTeamInfo
    {
        public DateTime StartDate { get; set; }

        public bool TeamIsInTournament { get; set; }
    }
}

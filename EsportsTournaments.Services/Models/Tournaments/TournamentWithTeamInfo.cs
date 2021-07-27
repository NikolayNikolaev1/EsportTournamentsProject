namespace EsportsTournaments.Services.Models.Tournaments
{
    using System;

    public class TournamentWithTeamInfo
    {
        public DateTime StartDate { get; set; }

        public bool TeamIsInTournament { get; set; }
    }
}

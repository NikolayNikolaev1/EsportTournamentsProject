namespace EsportsTournaments.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class Tournament
    {
        public int Id { get; set; }

        [MaxLength(TournamentNameMaxLength)]
        [MinLength(TournamentNameMinLength)]
        [Required]
        public string Name { get; set; }

        public PrizeType Prize { get; set; }

        public DateTime StartDate { get; set; }

        public bool HasStarted { get; set; }

        public bool HasEnded { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public IEnumerable<TeamTournament> Teams { get; set; } = new List<TeamTournament>();
    }
}

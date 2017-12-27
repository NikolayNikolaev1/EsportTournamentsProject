using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static EsportTournaments.Data.DataConstants;

namespace EsportTournaments.Data.Models
{
    public class Tournament
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TournamentNameMinLenght)]
        [MaxLength(MaxLenghtHundred)]
        public string Name { get; set; }

        public PrizeType Prize { get; set; }

        public DateTime StartDate { get; set; }

        public bool HasEnded { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public List<TeamTournament> Teams { get; set; } = new List<TeamTournament>();
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static EsportTournaments.Data.DataConstants;

namespace EsportTournaments.Data.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TeamNameMinLenght)]
        [MaxLength(TeamNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MinLength(TeamTagMinLenght)]
        [MaxLength(TeamTagMaxLenght)]
        [RegularExpression(LettersAndNumbersRgx)]
        public string Tag { get; set; }

        [Range(0, 1000)]
        public int TournamentsWon { get; set; }

        [Required]
        [MinLength(UrlMinLenght)]
        [MaxLength(UrlMaxLenght)]
        public string TeamImageUrl { get; set; }

        [Required]
        public string CaptainId { get; set; }

        public User Captain { get; set; }

        [Required]
        public int GameId { get; set; }

        public Game Game { get; set; }

        public List<PlayerTeam> Players { get; set; } = new List<PlayerTeam>();

        public List<TeamTournament> Tournaments { get; set; } = new List<TeamTournament>();
    }
}

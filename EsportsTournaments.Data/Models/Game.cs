using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static EsportTournaments.Data.DataConstants;

namespace EsportTournaments.Data.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxLenghtHundred)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxLenghtHundred)]
        public string Developer { get; set; }

        [Required]
        [MinLength(UrlMinLenght)]
        [MaxLength(UrlMaxLenght)]
        public string GameImageUrl { get; set; }

        [Required]
        [MinLength(UrlMinLenght)]
        [MaxLength(UrlMaxLenght)]
        public string GameWebsite { get; set; }

        public List<Team> Teams { get; set; } = new List<Team>();

        public List<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}

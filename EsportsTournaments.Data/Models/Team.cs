namespace EsportsTournaments.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class Team
    {
        public int Id { get; set; }

        [MaxLength(TeamNameMaxLength)]
        [MinLength(TeamNameMinLength)]
        [Required]
        public string Name { get; set; }

        [MaxLength(TeamTagMaxLength)]
        [MinLength(TeamTagMinLength)]
        [RegularExpression(@"\w+")]
        [Required]
        public string Tag { get; set; }

        [Range(TeamTournamentsWonMinLength, TeamTournamentsWonMaxLength)]
        public int TournamentsWon { get; set; }

        [MaxLength(ImageFileNameMaxLength)]
        [MinLength(ImageFileNameMinLength)]
        [Required]
        public string Image { get; set; }

        public string CaptainId { get; set; }

        public User Captain { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public IEnumerable<PlayerTeam> Players { get; set; } = new List<PlayerTeam>();

        public IEnumerable<TeamTournament> Tournaments { get; set; } = new List<TeamTournament>();
    }
}

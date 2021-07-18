namespace EsportsTournaments.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class Game
    {
        public int Id { get; set; }

        [MaxLength(GameNameMaxLength)]
        [MinLength(GameNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(GameDeveloperMaxLength)]
        [MinLength(GameDeveloperMinLength)]
        public string Developer { get; set; }

        [MaxLength(UrlMaxLength)]
        [MinLength(UrlMinLength)]
        [Required]
        public string GameImageUrl { get; set; }

        public IEnumerable<Team> Teams { get; set; } = new List<Team>();

        public IEnumerable<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}

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

        [MaxLength(GameDeveloperMaxLength)]
        [MinLength(GameDeveloperMinLength)]
        [Required]
        public string Developer { get; set; }

        [MaxLength(ImageFileNameMaxLength)]
        [MinLength(ImageFileNameMinLength)]
        [Required]
        public string Image { get; set; }

        public IEnumerable<Team> Teams { get; set; } = new List<Team>();

        public IEnumerable<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}

namespace EsportsTournaments.Web.Areas.Admin.Models.Games
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class AddGameFormModel
    {
        [MaxLength(GameNameMaxLength)]
        [MinLength(GameNameMinLength)]
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(GameDeveloperMaxLength)]
        [MinLength(GameDeveloperMinLength)]
        public string Developer { get; set; }

        [Display(Name = "Game Image URL")]
        [MaxLength(UrlMaxLength)]
        [MinLength(UrlMinLength)]
        [Required]
        public string GameImageUrl { get; set; }
    }
}

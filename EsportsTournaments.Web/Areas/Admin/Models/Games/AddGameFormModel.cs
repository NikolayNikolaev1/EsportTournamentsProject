namespace EsportsTournaments.Web.Areas.Admin.Models.Games
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;
    using static Common.WebConstants;

    public class AddGameFormModel
    {
        [MaxLength(GameNameMaxLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMaxLength)]
        [MinLength(GameNameMinLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMinLength)]
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(GameDeveloperMaxLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMaxLength)]
        [MinLength(GameDeveloperMinLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMinLength)]
        public string Developer { get; set; }

        [Display(Name = "Game Image URL")]
        [MaxLength(UrlMaxLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMaxLength)]
        [MinLength(UrlMinLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMinLength)]
        [Url]
        [Required]
        public string GameImageUrl { get; set; }
    }
}

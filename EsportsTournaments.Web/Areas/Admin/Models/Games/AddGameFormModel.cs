namespace EsportsTournaments.Web.Areas.Admin.Models.Games
{
    using Microsoft.AspNetCore.Http;
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

        [MaxLength(GameDeveloperMaxLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMaxLength)]
        [MinLength(GameDeveloperMinLength,
            ErrorMessage = ErrorMessages.InvalidPropertyMinLength)]
        [Required]
        public string Developer { get; set; }

        [Display(Name = "Game Image")]
        [Required]
        public IFormFile Image { get; set; }
    }
}

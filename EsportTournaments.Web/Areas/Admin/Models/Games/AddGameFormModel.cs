using System.ComponentModel.DataAnnotations;
using static EsportTournaments.Data.DataConstants;

namespace EsportTournaments.Web.Areas.Admin.Models.Games
{
    public class AddGameFormModel
    {
        [Required]
        [MaxLength(MaxLenghtHundred)]
        public string Name { get; set; }

        [Required]
        [MaxLength(MaxLenghtHundred)]
        public string Developer { get; set; }

        [Display(Name = "Game Image URL")]
        [Required]
        [MinLength(UrlMinLenght)]
        [MaxLength(UrlMaxLenght)]
        public string GameImageUrl { get; set; }

        [Display(Name = "Game Website")]
        [Required]
        [MinLength(UrlMinLenght)]
        [MaxLength(UrlMaxLenght)]
        public string GameWebsite { get; set; }
    }
}

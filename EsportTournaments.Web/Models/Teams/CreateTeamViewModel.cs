using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static EsportTournaments.Data.DataConstants;

namespace EsportTournaments.Web.Models.Teams
{
    public class CreateTeamViewModel
    {
        [Required]
        [MinLength(TeamNameMinLenght)]
        [MaxLength(TeamNameMaxLenght)]
        public string Name { get; set; }

        [Required]
        [MinLength(TeamTagMinLenght)]
        [MaxLength(TeamTagMaxLenght)]
        [RegularExpression(LettersAndNumbersRgx)]
        public string Tag { get; set; }

        [Required]
        [Display(Name = "Team Logo URL")]
        [MinLength(UrlMinLenght)]
        [MaxLength(UrlMaxLenght)]
        public string TeamImageUrl { get; set; }

        [Display(Name = "Game")]
        [Required]
        public int GameId { get; set; }

        public IEnumerable<SelectListItem> Games { get; set; }
    }
}

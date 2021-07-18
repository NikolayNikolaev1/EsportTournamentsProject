namespace EsportsTournaments.Web.Models.Teams
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants;

    public class CreateTeamViewModel
    {
        [MaxLength(TeamNameMinLength)]
        [MinLength(TeamNameMaxLength)]
        [Required]
        public string Name { get; set; }

        [MaxLength(TeamTagMaxLength)]
        [MinLength(TeamTagMinLength)]
        [RegularExpression(@"\w+")]
        [Required]
        public string Tag { get; set; }

        [Display(Name = "Team Logo URL")]
        [MaxLength(UrlMaxLength)]
        [MinLength(UrlMinLength)]
        [Required]
        public string TeamImageUrl { get; set; }

        [Display(Name = "Game")]
        [Required]
        public int GameId { get; set; }

        public IEnumerable<SelectListItem> Games { get; set; }
    }
}

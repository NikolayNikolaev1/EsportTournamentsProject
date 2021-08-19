namespace EsportsTournaments.Web.Models.Teams
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Common.DataConstants;

    public class CreateTeamViewModel
    {
        [MaxLength(TeamNameMaxLength)]
        [MinLength(TeamNameMinLength)]
        [Required]
        public string Name { get; set; }

        [MaxLength(TeamTagMaxLength)]
        [MinLength(TeamTagMinLength)]
        [RegularExpression(@"\w+")]
        [Required]
        public string Tag { get; set; }

        [Display(Name = "Team Logo")]
        [Required]
        public IFormFile Image { get; set; }

        [Display(Name = "Game")]
        [Required]
        public int GameId { get; set; }

        public IEnumerable<SelectListItem> Games { get; set; }
    }
}

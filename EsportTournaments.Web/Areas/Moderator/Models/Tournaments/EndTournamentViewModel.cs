using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EsportTournaments.Web.Areas.Moderator.Models.Tournaments
{
    public class EndTournamentViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Team")]
        [Required]
        public int TeamId { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }
    }
}

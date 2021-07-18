namespace EsportsTournaments.Web.Areas.Moderator.Models.Tournaments
{
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ManageTournamentViewModel
    {
        public int Id { get; set; }

        public Tournament Tournament { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }

        [Display(Name = "Teams")]
        [Required]
        public int TeamId { get; set; }
    }
}

using EsportTournaments.Data.Models;
using EsportTournaments.Services.Moderator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EsportTournaments.Web.Areas.Moderator.Models.Tournaments
{
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

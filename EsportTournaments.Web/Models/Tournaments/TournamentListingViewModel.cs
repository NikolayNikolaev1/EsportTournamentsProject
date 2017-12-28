using EsportTournaments.Services.Models;
using System;
using System.Collections.Generic;

namespace EsportTournaments.Web.Models.Tournaments
{
    public class TournamentListingViewModel
    {
        public IEnumerable<TournamentListingServiceModel> Tournaments { get; set; }

        public int TotalTournaments { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalTournaments / 6);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}

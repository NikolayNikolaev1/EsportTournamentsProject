namespace EsportsTournaments.Web.Models.Tournaments
{
    using Services.Models.Tournaments;
    using System;
    using System.Collections.Generic;

    using static Common.WebConstants;

    public class TournamentListingViewModel
    {
        public IEnumerable<TournamentListingServiceModel> Tournaments { get; set; }

        public int TotalTournaments { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalTournaments / PaginationSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}

namespace EsportsTournaments.Web.Models.Teams
{
    using Services.Models;
    using System;
    using System.Collections.Generic;

    public class TeamListingViewModel
    {
        public IEnumerable<TeamListingServiceModel> Teams { get; set; }

        public int TotalTournaments { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalTournaments / 6);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}

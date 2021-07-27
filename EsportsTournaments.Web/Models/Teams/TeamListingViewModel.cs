namespace EsportsTournaments.Web.Models.Teams
{
    using Services.Models.Teams;
    using System;
    using System.Collections.Generic;

    using static Common.WebConstants;

    public class TeamListingViewModel
    {
        public IEnumerable<TeamListingServiceModel> Teams { get; set; }

        public int TotalTeams { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalTeams / PaginationSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}

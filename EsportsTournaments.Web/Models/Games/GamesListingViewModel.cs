namespace EsportsTournaments.Web.Models.Games
{
    using Services.Models.Games;
    using System;
    using System.Collections.Generic;

    using static Common.WebConstants;

    public class GamesListingViewModel
    {
        public IEnumerable<GameListingServiceModel> Games { get; set; }

        public int TotalGames { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalGames / PaginationSize);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}

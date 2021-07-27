namespace EsportsTournaments.Services.Models.Games
{
    using Core.Mapping;
    using Data.Models;

    public class GameListingServiceModel : IMapFrom<Game>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Developer { get; set; }

        public string GameImageUrl { get; set; }
    }
}

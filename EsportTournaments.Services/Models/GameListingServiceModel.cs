using EsportTournaments.Core.Mapping;
using EsportTournaments.Data.Models;

namespace EsportTournaments.Services.Models
{
    public class GameListingServiceModel : IMapFrom<Game>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Developer { get; set; }

        public string GameImageUrl { get; set; }

        public string GameWebsite { get; set; }
    }
}

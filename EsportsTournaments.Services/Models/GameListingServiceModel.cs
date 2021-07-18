using EsportsTournaments.Core.Mapping;
using EsportsTournaments.Data.Models;

namespace EsportsTournaments.Services.Models
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

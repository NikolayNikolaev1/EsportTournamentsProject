using EsportsTournaments.Core.Mapping;
using EsportsTournaments.Data.Models;

namespace EsportsTournaments.Services.Moderator.Models
{
    public class TeamInTournamentServiceModel : IMapFrom<Team>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

using EsportTournaments.Core.Mapping;
using EsportTournaments.Data.Models;

namespace EsportTournaments.Services.Moderator.Models
{
    public class TeamInTournamentServiceModel : IMapFrom<Team>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

using EsportTournaments.Core.Mapping;
using EsportTournaments.Data.Models;
using System;

namespace EsportTournaments.Services.Moderator.Models
{
    public class ModeratorTournamentManageServiceModel : IMapFrom<Tournament>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PrizeType Prize { get; set; }
        
        public DateTime StartDate { get; set; }

        public bool HasStarted { get; set; }

        public bool HasEnded { get; set; }
    }
}

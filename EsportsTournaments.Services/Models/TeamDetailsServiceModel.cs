using AutoMapper;
using EsportTournaments.Core.Mapping;
using EsportTournaments.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace EsportTournaments.Services.Models
{
    public class TeamDetailsServiceModel : IMapFrom<Team>, IMapFrom<User>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public int TournamentsWon { get; set; }

        public string TeamImageUrl { get; set; }

        public string Captain { get; set; }

        public string Game { get; set; }

        public IEnumerable<string> Players { get; set; }

        public void ConfigureMapping(Profile mapper)
             => mapper
                     .CreateMap<Team, TeamDetailsServiceModel>()
                     .ForMember(t => t.Captain, cfg => cfg.MapFrom(t => t.Captain.UserName))
                     .ForMember(t => t.Game, cfg => cfg.MapFrom(t => t.Game.Name))
                     .ForMember(t => t.Players, cfg => cfg.MapFrom(t => t.Players.Select(p => p.Player.UserName)));
    }
}

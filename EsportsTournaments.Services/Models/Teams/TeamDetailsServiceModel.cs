﻿namespace EsportsTournaments.Services.Models.Teams
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class TeamDetailsServiceModel : IMapFrom<Team>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public int TournamentsWon { get; set; }

        public string Image { get; set; }

        public string Captain { get; set; }

        public IEnumerable<string> Players { get; set; }

        public void ConfigureMapping(Profile mapper)
             => mapper
                     .CreateMap<Team, TeamDetailsServiceModel>()
                     .ForMember(t => t.Captain, cfg => cfg.MapFrom(t => t.Captain.UserName))
                     .ForMember(t => t.Players, cfg => cfg.MapFrom(t => t.Players.Select(p => p.Player.UserName)));
    }
}

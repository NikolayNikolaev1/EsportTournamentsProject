namespace EsportsTournaments.Services.Models.Tournaments
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TournamentDetailsServiceModel : IMapFrom<Tournament>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PrizeType Prize { get; set; }

        public DateTime StartDate { get; set; }

        public string Game { get; set; }

        public string GameImage { get; set; }

        public IEnumerable<string> Teams { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                    .CreateMap<Tournament, TournamentDetailsServiceModel>()
                    .ForMember(t => t.Game, cfg => cfg.MapFrom(t => t.Game.Name))
                    .ForMember(t => t.GameImage, cfg => cfg.MapFrom(t => t.Game.GameImageUrl))
                    .ForMember(t => t.Teams, cfg => cfg.MapFrom(t => t.Teams.Select(teams => teams.Team.Name)));
    }
}

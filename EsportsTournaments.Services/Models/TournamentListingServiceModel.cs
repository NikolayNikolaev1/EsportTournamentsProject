using AutoMapper;
using EsportTournaments.Core.Mapping;
using EsportTournaments.Data.Models;
using System;

namespace EsportTournaments.Services.Models
{
    public class TournamentListingServiceModel : IMapFrom<Tournament>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PrizeType Prize { get; set; }

        public DateTime StartDate { get; set; }

        public string Game { get; set; }

        public string GameImage { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                    .CreateMap<Tournament, TournamentListingServiceModel>()
                    .ForMember(t => t.Game, cfg => cfg.MapFrom(t => t.Game.Name))
                    .ForMember(t => t.GameImage, cfg => cfg.MapFrom(t => t.Game.GameImageUrl));
    }
}

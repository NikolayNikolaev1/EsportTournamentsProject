namespace EsportsTournaments.Services.Models.Teams
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;

    public class TeamListingServiceModel : IMapFrom<Team>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string TeamImageUrl { get; set; }

        public string Game { get; set; }

        public void ConfigureMapping(Profile mapper)
             => mapper
                     .CreateMap<Team, TeamListingServiceModel>()
                     .ForMember(t => t.Game, cfg => cfg.MapFrom(t => t.Game.Name));
    }
}

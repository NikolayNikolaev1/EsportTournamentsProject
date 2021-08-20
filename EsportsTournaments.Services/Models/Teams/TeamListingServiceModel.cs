namespace EsportsTournaments.Services.Models.Teams
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Linq;

    public class TeamListingServiceModel : IMapFrom<Team>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Image { get; set; }

        public int PlayersCount { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Team, TeamListingServiceModel>()
            .ForMember(t => t.PlayersCount, cfg => cfg.MapFrom(t => t.Players.Count()));
    }
}

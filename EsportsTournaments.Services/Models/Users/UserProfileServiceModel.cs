namespace EsportsTournaments.Services.Models.Users
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class UserProfileServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Username { get; set; }

        public string ProfilePictureUrl { get; set; }

        public IEnumerable<UserProfileTeamsServiceModel> Teams { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<User, UserProfileServiceModel>()
            .ForMember(u => u.Teams, cfg => cfg.MapFrom(p => p.Teams.Select(t => t.Team)));
    }
}

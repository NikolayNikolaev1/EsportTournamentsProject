using AutoMapper;
using EsportTournaments.Core.Mapping;
using EsportTournaments.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace EsportTournaments.Services.Models
{
    public class UserProfileServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Username { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Country { get; set; }

        public string LeagueOfLegendsAccount { get; set; }

        public string SteamAccount { get; set; }

        public string BlizzardAccount { get; set; }

        public IEnumerable<UserProfileTeamsServiceModel> Teams { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<User, UserProfileServiceModel>()
            .ForMember(u => u.Teams, cfg => cfg.MapFrom(p => p.Teams.Select(t => t.Team)));
    }
}

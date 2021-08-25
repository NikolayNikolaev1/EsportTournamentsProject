namespace EsportsTournaments.Services.Models.Teams
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Users;

    public class TeamListingServiceModel : IMapFrom<Team>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Image { get; set; }

        public int TournamentsWon { get; set; }

        public UserListingServiceModel Captain { get; set; }

        public IEnumerable<UserListingServiceModel> Players { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<Team, TeamListingServiceModel>()
            .ForMember(t => t.Captain,
                cfg => cfg.MapFrom(t => new UserListingServiceModel
                {
                    Id = t.Captain.Id,
                    Username = t.Captain.UserName,
                    ProfilePicture = t.Captain.ProfilePicture
                }))
            .ForMember(t => t.Players,
                cfg => cfg.MapFrom(t => t.Players
                .Select(p => new UserListingServiceModel
                {
                    Id = p.PlayerId,
                    Username = p.Player.UserName,
                    ProfilePicture = p.Player.ProfilePicture
                })
                .ToList()));
    }
}

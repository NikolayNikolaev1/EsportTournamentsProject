namespace EsportsTournaments.Services.Models.Users
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Linq;

    public class UserProfileServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Username { get; set; }

        public string ProfilePicture { get; set; }

        public int TotalTournamentsWon { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<User, UserProfileServiceModel>()
            .ForMember(u => u.TotalTournamentsWon,
                cfg => cfg.MapFrom(p => p.Teams
                .Select(t => t.Team.TournamentsWon)
                .Sum()));
    }
}

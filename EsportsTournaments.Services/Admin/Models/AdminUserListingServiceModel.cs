namespace EsportsTournaments.Services.Admin.Models
{
    using AutoMapper;
    using Core.Mapping;
    using Data.Models;
    using System.Linq;

    public class AdminUserListingServiceModel : IMapFrom<User>, IHaveCustomMapping
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }

        public void ConfigureMapping(Profile profile)
            => profile
            .CreateMap<User, AdminUserListingServiceModel>()
            .ForMember(p => p.Role,
                cfg => cfg.MapFrom(u => u.Roles.FirstOrDefault().Role));
    }
}

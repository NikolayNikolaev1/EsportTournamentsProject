namespace EsportsTournaments.Services.Models.Users
{
    using Core.Mapping;
    using Data.Models;

    public class UserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string ProfilePicture { get; set; }
    }
}

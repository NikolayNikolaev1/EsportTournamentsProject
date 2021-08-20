namespace EsportsTournaments.Web.Models.Users
{
    using Services.Models.Teams;
    using Services.Models.Users;
    using System.Collections.Generic;

    public class UserProfileViewModel
    {
        public UserProfileServiceModel UserInfo { get; set; }

        public IEnumerable<TeamListingServiceModel> CreatedTeams { get; set; }

        public IEnumerable<TeamListingServiceModel> JoinedTeams { get; set; }
    }
}

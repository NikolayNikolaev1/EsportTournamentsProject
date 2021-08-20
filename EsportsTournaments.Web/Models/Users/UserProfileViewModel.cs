namespace EsportsTournaments.Web.Models.Users
{
    using Services.Models.Teams;
    using Services.Models.Users;
    using System.Collections.Generic;

    public class UserProfileViewModel
    {
        public UserProfileServiceModel UserInfo { get; set; }

        public IEnumerable<TeamListingServiceModel> CaptainTeams { get; set; }

        public IEnumerable<TeamListingServiceModel> MemberTeams { get; set; }
    }
}

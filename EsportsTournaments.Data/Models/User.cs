namespace EsportsTournaments.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string ProfilePicture { get; set; }

        public IEnumerable<Team> TeamsLeading { get; set; }

        public IEnumerable<PlayerTeam> Teams { get; set; } = new List<PlayerTeam>();

        public IEnumerable<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}

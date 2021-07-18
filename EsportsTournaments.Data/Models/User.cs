namespace EsportsTournaments.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public IEnumerable<Team> TeamsLeading { get; set; }

        public IEnumerable<PlayerTeam> Teams { get; set; } = new List<PlayerTeam>();
    }
}

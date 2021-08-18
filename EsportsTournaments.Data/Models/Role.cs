namespace EsportsTournaments.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class Role : IdentityRole
    {
        public IEnumerable<UserRole> Users { get; set; }
    }
}

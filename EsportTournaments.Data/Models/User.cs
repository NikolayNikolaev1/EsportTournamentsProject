using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static EsportTournaments.Data.DataConstants;

namespace EsportTournaments.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(MaxLenghtHundred)]
        public string Country { get; set; }

        [MinLength(LoLAccMinLenght)]
        [MaxLength(LoLAccMaxLenght)]
        [RegularExpression(LoLAccRgx)]
        public string LeagueOfLegendsAccount { get; set; }

        [MaxLength(MaxLenghtHundred)]
        public string SteamAccount { get; set; }

        [MinLength(BlizzardAccMinLenght)]
        [MaxLength(BlizzardAccMaxLenght)]
        [RegularExpression(LettersAndNumbersRgx)]
        public string BlizzardAccount { get; set; }

        public List<Team> CaptainOfTeams { get; set; }

        public List<PlayerTeam> Teams { get; set; } = new List<PlayerTeam>();
    }
}

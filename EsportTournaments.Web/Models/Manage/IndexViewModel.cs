using System.ComponentModel.DataAnnotations;
using static EsportTournaments.Data.DataConstants;

namespace EsportTournaments.Web.Models.Manage
{
    public class IndexViewModel
    {
        [MaxLength(MaxLenghtHundred)]
        public string Country { get; set; }

        [MinLength(UrlMinLenght)]
        [MaxLength(UrlMaxLenght)]
        [Display(Name = "Profile Picture URL")]
        public string ProfilePictureUrl { get; set; }

        [MinLength(LoLAccMinLenght)]
        [MaxLength(LoLAccMaxLenght)]
        [RegularExpression(LoLAccRgx)]
        [Display(Name = "League of Legends Account")]
        public string LeagueOfLegendsAccount { get; set; }

        [MaxLength(MaxLenghtHundred)]
        [Display(Name = "Steam Account")]
        public string SteamAccount { get; set; }

        [MinLength(BlizzardAccMinLenght)]
        [MaxLength(BlizzardAccMaxLenght)]
        [RegularExpression(LettersAndNumbersRgx)]
        [Display(Name = "Blizzard Account")]
        public string BlizzardAccount { get; set; }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}

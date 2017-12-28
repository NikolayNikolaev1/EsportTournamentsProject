using System.ComponentModel.DataAnnotations;
using static EsportTournaments.Data.DataConstants;

namespace EsportTournaments.Web.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [MaxLength(MaxLenghtHundred)]
        public string Country { get; set; }

        [MinLength(LoLAccMinLenght)]
        [MaxLength(LoLAccMaxLenght)]
        [RegularExpression(LoLAccRgx)]
        [Display(Name = "League Of Legends Account Username")]
        public string LeagueOfLegendsAccount { get; set; }

        [MaxLength(MaxLenghtHundred)]
        [Display(Name = "Steam Account Username")]
        public string SteamAccount { get; set; }

        [MinLength(BlizzardAccMinLenght)]
        [MaxLength(BlizzardAccMaxLenght)]
        [RegularExpression(LettersAndNumbersRgx)]
        [Display(Name = "Blizzard Account Username")]
        public string BlizzardAccount { get; set; }
    }
}

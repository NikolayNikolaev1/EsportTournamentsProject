namespace EsportsTournaments.Web.Models.Manage
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class IndexViewModel
    {
        [Display(Name = "Profile Picture URL")]
        [MaxLength(UrlMaxLength)]
        [MinLength(UrlMinLength)]
        public string ProfilePictureUrl { get; set; }

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

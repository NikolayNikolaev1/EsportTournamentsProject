using System.ComponentModel.DataAnnotations;

namespace EsportsTournaments.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

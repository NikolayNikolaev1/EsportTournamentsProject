using System.ComponentModel.DataAnnotations;

namespace EsportTournaments.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

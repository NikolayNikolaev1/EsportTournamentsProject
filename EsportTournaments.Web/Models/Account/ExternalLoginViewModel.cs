using System.ComponentModel.DataAnnotations;

namespace EsportTournaments.Web.Models.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

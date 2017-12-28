using System.ComponentModel.DataAnnotations;

namespace EsportTournaments.Web.Areas.Admin.Models.Users
{
    public class AddUserToFormViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}

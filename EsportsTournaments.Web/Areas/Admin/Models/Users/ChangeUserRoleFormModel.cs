namespace EsportsTournaments.Web.Areas.Admin.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class ChangeUserRoleFormModel
    {
        [Required]
        public string UserId { get; set; }

        public string CurrentRole { get; set; }

        [Required]
        public string Role { get; set; }
    }
}

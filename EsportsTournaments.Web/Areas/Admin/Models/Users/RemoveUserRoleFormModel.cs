namespace EsportsTournaments.Web.Areas.Admin.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class RemoveUserRoleFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string CurrentRole { get; set; }
    }
}

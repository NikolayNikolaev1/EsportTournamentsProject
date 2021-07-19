namespace EsportsTournaments.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.WebConstants;

    [Area(Areas.Admin)]
    [Authorize(Roles = Roles.Administrator)]
    public abstract class BaseAdminController : Controller
    {
    }
}

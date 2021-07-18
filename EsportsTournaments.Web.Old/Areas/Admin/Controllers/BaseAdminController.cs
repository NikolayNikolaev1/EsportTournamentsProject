using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EsportsTournaments.Core.WebConstants;

namespace EsportsTournaments.Web.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public abstract class BaseAdminController : Controller
    {
    }
}

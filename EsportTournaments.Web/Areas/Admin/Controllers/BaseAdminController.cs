using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EsportTournaments.Core.WebConstants;

namespace EsportTournaments.Web.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public abstract class BaseAdminController : Controller
    {
    }
}

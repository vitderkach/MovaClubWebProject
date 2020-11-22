using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovaClubWebApp.Areas.Owner.Controllers
{
    [Authorize(Roles = "Owner")]
    [Area("Owner")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
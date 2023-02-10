using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
                return View();

            return RedirectToAction("AccessDenied", "Admin");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

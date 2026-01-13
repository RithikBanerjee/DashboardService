using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Dashboard.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("DefaultDashboard");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View("AdminDashboard");
        }

        [Authorize(Roles = "User")]
        public IActionResult User()
        {
            return View("UserDashboard");
        }
    }
}

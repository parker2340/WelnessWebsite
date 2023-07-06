
using Microsoft.AspNetCore.Mvc;

namespace WelnessWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

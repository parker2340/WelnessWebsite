using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace WelnessWebsite.Controllers
{
    public class LoginPage : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("LoginPage/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("LoginPage/SignUp")]
        public IActionResult SignUpPage()
        {
            return View();
        }
    }
}

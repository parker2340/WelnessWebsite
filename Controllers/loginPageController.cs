using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace WelnessWebsite.Controllers;


public class LoginPage : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult login()
    {
        return View();
    }
 
}

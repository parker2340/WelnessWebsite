using Microsoft.AspNetCore.Mvc;
using WelnessWebsite.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WelnessWebsite.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace WelnessWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly WelnessWebsiteContext _context;

        public HomeController(WelnessWebsiteContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return View();
            }

            var today = DateTime.Today;

            var user = _context.User
                .Where(u => u.ID == userId)
                .Include(u => u.DailyWorkout)
                .ThenInclude(dw => dw.Workout)
                .Include(u => u.WeeklyNutrition)
                .ThenInclude(wn => wn.DailyNutrition)
                .ThenInclude(n => n.Nutrition)
                .FirstOrDefault();



            if (user == null)
            {
                return View();
            }

            var dailyWorkout = user.DailyWorkout
                .FirstOrDefault(dw => dw.DateTime != null && dw.DateTime.Date == today.Date);

            var dailyNutrition = user.WeeklyNutrition
                .SelectMany(wn => wn.DailyNutrition)
                .Where(dn => dn.DateTime != null && dn.DateTime.Date == today.Date.Date)
                .FirstOrDefault();

            var weeklyNutrition = user.WeeklyNutrition
                .Where(dn => dn.WeekNumber != null && dn.WeekNumber == CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Today, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                .FirstOrDefault();


            return View(new HomeView
            {
                User = user,
                DailyWorkout = dailyWorkout,
                DailyNutrition = dailyNutrition,
                WeeklyNutrition = weeklyNutrition
            });
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");

        }



    }
}

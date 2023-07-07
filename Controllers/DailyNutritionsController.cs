using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WelnessWebsite.Data;
using WelnessWebsite.Models;


namespace WelnessWebsite.Controllers
{
    public class DailyNutritionsController : Controller
    {
        private readonly WelnessWebsiteContext _context;

        public DailyNutritionsController(WelnessWebsiteContext context)
        {
            _context = context;
        }

        // GET: DailyNutritions
        public async Task<IActionResult> Index()
        {
            if (_context.DailyNutrition == null)
            {
                return Problem("Entity set 'WelnessWebsiteContext.DailyNutrition' is null.");
            }

            var dailyNutrition = await _context.DailyNutrition
                .Include(dn => dn.Nutrition) // Include the Nutrition table
                .ToListAsync();

            // Calculate the sum of the Nutrition column for each DailyNutrition row
            foreach (var item in dailyNutrition)
            {
                item.Calories = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.Calories);

                item.serving_size_g = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.serving_size_g);

                item.fat_total_g = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.fat_total_g);

                item.fat_saturated_g = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.fat_saturated_g);

                item.protein_g = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.protein_g);

                item.sodium_mg = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.sodium_mg);

                item.potassium_mg = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.potassium_mg);

                item.cholesterol_mg = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.cholesterol_mg);

                item.carbohydrates_total_g = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.carbohydrates_total_g);

                item.fiber_g = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.fiber_g);

                item.sugar_g = _context.Nutrition
                    .Where(n => n.DailyNutritionID == item.ID)
                    .Sum(n => n.sugar_g);
            }

            return View(dailyNutrition);
        }

        // GET: DailyNutritions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DailyNutrition == null)
            {
                return NotFound();
            }

            var dailyNutrition = await _context.DailyNutrition
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dailyNutrition == null)
            {
                return NotFound();
            }

            return View(dailyNutrition);
        }


        // POST: DailyNutritions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            // Get the current week number and year
            int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(DateTime.Today, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int year = DateTime.Today.Year;

            // Check if WeeklyNutrition record exists for the current week and year
            var weeklyNutrition = _context.WeeklyNutrition.FirstOrDefault(wn => wn.WeekNumber == weekNumber && wn.Year == year);

            if (weeklyNutrition == null)
            {
                var userId = HttpContext.Session.GetInt32("UserId");

                // Create a new WeeklyNutrition record for the current week and year
                weeklyNutrition = new WeeklyNutrition
                {
                    UserId = userId.Value,
                    WeekNumber = weekNumber,
                    Year = year
                };
                _context.WeeklyNutrition.Add(weeklyNutrition);
                await _context.SaveChangesAsync();
            }

            // Check if DailyNutrition record exists for the current date
            var dailyNutrition = _context.DailyNutrition.FirstOrDefault(dn => dn.DateTime.Date == DateTime.Today);

            if (dailyNutrition == null)
            {
                // Create a new DailyNutrition record with default values
                dailyNutrition = new DailyNutrition
                {
                    WeeklyID = weeklyNutrition.ID,
                    Calories = 0,
                    serving_size_g = 0,
                    fat_total_g = 0,
                    fat_saturated_g = 0,
                    protein_g = 0,
                    sodium_mg = 0,
                    potassium_mg = 0,
                    cholesterol_mg = 0,
                    carbohydrates_total_g = 0,
                    fiber_g = 0,
                    sugar_g = 0,
                    DateTime = DateTime.Today
                };
                _context.DailyNutrition.Add(dailyNutrition);
                await _context.SaveChangesAsync();

                // Redirect to the Nutriants/Search page with the DailyNutrition ID
                return RedirectToAction("Search", "Nutritions", new { id = dailyNutrition.ID });
            }

            // Redirect to the Nutriants/Search page without creating a new DailyNutrition
            return RedirectToAction("Search", "Nutritions", new { id = dailyNutrition.ID });
        }


        // GET: DailyNutritions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DailyNutrition == null)
            {
                return NotFound();
            }

            var dailyNutrition = await _context.DailyNutrition.FindAsync(id);
            if (dailyNutrition == null)
            {
                return NotFound();
            }
            return View(dailyNutrition);
        }

        // POST: DailyNutritions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeeklyID,ID,Name,Calories,serving_size_g,fat_total_g,fat_saturated_g,protein_g,sodium_mg,potassium_mg,cholesterol_mg,carbohydrates_total_g,fiber_g,sugar_g,DateTime")] DailyNutrition dailyNutrition)
        {
            if (id != dailyNutrition.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyNutrition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyNutritionExists(dailyNutrition.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dailyNutrition);
        }

        // GET: DailyNutritions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DailyNutrition == null)
            {
                return NotFound();
            }

            var dailyNutrition = await _context.DailyNutrition
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dailyNutrition == null)
            {
                return NotFound();
            }

            return View(dailyNutrition);
        }

        // POST: DailyNutritions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DailyNutrition == null)
            {
                return Problem("Entity set 'WelnessWebsiteContext.DailyNutrition'  is null.");
            }
            var dailyNutrition = await _context.DailyNutrition.FindAsync(id);
            if (dailyNutrition != null)
            {
                _context.DailyNutrition.Remove(dailyNutrition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyNutritionExists(int id)
        {
            return (_context.DailyNutrition?.Any(e => e.ID == id)).GetValueOrDefault();
        }

       
    }
}

﻿using System;
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
            var userID = HttpContext.Session.GetInt32("UserId").Value;

            var weeklyNutritions = await _context.WeeklyNutrition
                .Where(wn => wn.UserId == userID)
                .Include(wn => wn.DailyNutrition)
                .ThenInclude(dn => dn.Nutrition)
                .ToListAsync();

            var dailyNutritions = weeklyNutritions
                .SelectMany(wn => wn.DailyNutrition)
                .ToList();


            return View(dailyNutritions);
        }

        // GET: DailyNutritions/Details/5
        public IActionResult Details(int id)
        {
            // Retrieve the daily workout with the specified ID
            var dailynutrition = _context.DailyNutrition
                .Include(dw => dw.Nutrition)
                .FirstOrDefault(dw => dw.ID == id);

            if (dailynutrition == null)
            {
                return NotFound();
            }

            // Retrieve the workouts associated with the daily workout
            var Nutrition = dailynutrition.Nutrition;

            return View(Nutrition);
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

            var userId = HttpContext.Session.GetInt32("UserId");

            // Check if WeeklyNutrition record exists for the current week, year, and user ID
            var weeklyNutrition = _context.WeeklyNutrition.FirstOrDefault(wn => wn.UserId == userId && wn.WeekNumber == weekNumber && wn.Year == year);

            if (weeklyNutrition == null)
            {
                // Create a new WeeklyNutrition record for the current week, year, and user ID
                weeklyNutrition = new WeeklyNutrition
                {
                    UserId = userId.Value,
                    WeekNumber = weekNumber,
                    Year = year
                };
                _context.WeeklyNutrition.Add(weeklyNutrition);
                await _context.SaveChangesAsync();
            }

            // Check if DailyNutrition record exists for the current date and WeeklyNutrition ID
            var dailyNutrition = _context.DailyNutrition.FirstOrDefault(dn => dn.DateTime.Date == DateTime.Today && dn.WeeklyNutritionID == weeklyNutrition.ID);

            if (dailyNutrition == null)
            {
                // Create a new DailyNutrition record with default values
                dailyNutrition = new DailyNutrition
                {
                    WeeklyNutritionID = weeklyNutrition.ID,
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
                    DateTime = DateTime.Today,
                };
                _context.DailyNutrition.Add(dailyNutrition);
                await _context.SaveChangesAsync();

                // Redirect to the Nutriants/Search page with the DailyNutrition ID
                return RedirectToAction("Search", "Nutritions", new { id = dailyNutrition.ID });
            }

            // Redirect to the Nutriants/Search page without creating a new DailyNutrition
            return RedirectToAction("Search", "Nutritions", new { id = dailyNutrition.ID });
        }



/*        // GET: DailyNutritions/Edit/5
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
        }*/

        // POST: DailyNutritions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (_context.Workout == null)
            {
                return Problem("Entity set 'WelnessWebsiteContext.Workout'  is null.");
            }
            var Nutrition = await _context.Nutrition.FindAsync(id);
            if (Nutrition != null)
            {
                _context.Nutrition.Remove(Nutrition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

using System;
using System.Collections.Generic;
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
            return _context.DailyNutrition != null ?
                        View(await _context.DailyNutrition.ToListAsync()) :
                        Problem("Entity set 'WelnessWebsiteContext.DailyNutrition'  is null.");
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

        // GET: DailyNutritions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DailyNutritions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeeklyID,ID,Name,Calories,serving_size_g,fat_total_g,fat_saturated_g,protein_g,sodium_mg,potassium_mg,cholesterol_mg,carbohydrates_total_g,fiber_g,sugar_g,DateTime")] DailyNutrition dailyNutrition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyNutrition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyNutrition);
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
        // GET: DailyNutritions/Search
        public IActionResult Search(int ID)
        {

            HttpContext.Session.SetInt32("WeeklyNutrition", ID);

            return View();
        }

        // POST: DailyNutritions/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("DailyNutritions", "The Name field is required.");
                return View();
            }

            string api_url = $"https://api.api-ninjas.com/v1/nutrition?query={name}";
            string apiKey = "SsgKeVWXoIqUGk49rXOFiHvhnF55d2yWEXKd8KxB";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(api_url);
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    List<DailyNutrition> DailyNutrition = JsonConvert.DeserializeObject<List<DailyNutrition>>(responseContent);

                    return View("Search", DailyNutrition);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while searching for Nutrition. Please try again later.");
                    return View();
                }
            }
        }
    }
}

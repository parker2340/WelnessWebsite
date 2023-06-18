using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WelnessWebsite.Data;
using WelnessWebsite.Models;

namespace WelnessWebsite.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly WelnessWebsiteContext _context;

        public WorkoutsController(WelnessWebsiteContext context)
        {
            _context = context;
        }

        // GET: Workouts
        public async Task<IActionResult> Index()
        {
            if (_context.Workout != null)
            {
                var workouts = await _context.Workout.ToListAsync();
                return View(workouts);
            }
            else
            {
                return Problem("Entity set 'WelnessWebsiteContext.Workout' is null.");
            }
        }

        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Workout == null)
            {
                return NotFound();
            }

            var workout = await _context.Workout.FirstOrDefaultAsync(m => m.ID == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workouts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,Type,Muscle,Difficulty,Instructions")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Workout == null)
            {
                return NotFound();
            }

            var workout = await _context.Workout.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,Type,Muscle,Difficulty,Instructions")] Workout workout)
        {
            if (id != workout.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutExists(workout.ID))
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
            return View(workout);
        }

        // GET: Workouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Workout == null)
            {
                return NotFound();
            }

            var workout = await _context.Workout.FirstOrDefaultAsync(m => m.ID == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Workout == null)
            {
                return Problem("Entity set 'WelnessWebsiteContext.Workout' is null.");
            }

            var workout = await _context.Workout.FindAsync(id);
            if (workout != null)
            {
                _context.Workout.Remove(workout);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workout.Any(e => e.ID == id);
        }

        // GET: Workouts/Search
        public IActionResult Search()
        {
            return View();
        }

        // POST: Workouts/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string muscle)
        {
            if (string.IsNullOrEmpty(muscle))
            {
                ModelState.AddModelError("Muscle", "The Muscle field is required.");
                return View();
            }

            string api_url = $"https://api.api-ninjas.com/v1/exercises?muscle={muscle}";
            string apiKey = "SsgKeVWXoIqUGk49rXOFiHvhnF55d2yWEXKd8KxB";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);

                try
                {
                    HttpResponseMessage response = await client.GetAsync(api_url);
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    List<Workout> workouts = JsonConvert.DeserializeObject<List<Workout>>(responseContent);

                    return View("Search", workouts);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while searching for workouts. Please try again later.");
                    return View();
                }
            }
        }
    }
}
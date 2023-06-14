using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WelnessWebsite.Data;

namespace WelnessWebsite.Models
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
              return _context.Workout != null ? 
                          View(await _context.Workout.ToListAsync()) :
                          Problem("Entity set 'WelnessWebsiteContext.Workout'  is null.");
        }

        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Workout == null)
            {
                return NotFound();
            }

            var workout = await _context.Workout
                .FirstOrDefaultAsync(m => m.ID == id);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,type,muscle,dificulty,instructions")] Workout workout)
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,type,muscle,dificulty,instructions")] Workout workout)
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

            var workout = await _context.Workout
                .FirstOrDefaultAsync(m => m.ID == id);
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
                return Problem("Entity set 'WelnessWebsiteContext.Workout'  is null.");
            }
            var workout = await _context.Workout.FindAsync(id);
            if (workout != null)
            {
                _context.Workout.Remove(workout);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutExists(int id)
        {
          return (_context.Workout?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        // GET: Workouts/Search
        public ActionResult Search()
        {
            List<Workout> workouts = new List<Workout>(); // Initialize with an empty list

            // Create an instance of HttpClient
            HttpClient httpClient = null;

            try
            {
                httpClient = new HttpClient();

                // Set the base URL of the API
                httpClient.BaseAddress = new Uri("https://api.api-ninjas.com/v1/exercises");

                // Set the API key in the request headers
                httpClient.DefaultRequestHeaders.Add("Authorization", "SsgKeVWXoIqUGk49rXOFiHvhnF55d2yWEXKd8KxB");

                // Make the API call and retrieve the response
                HttpResponseMessage response = httpClient.GetAsync("workouts").GetAwaiter().GetResult();

                // Check if the response is null
                if (response != null)
                {
                    // If the API call is successful (status code 200), parse the response
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                        // Deserialize the response content into a list of Workout
                        workouts = JsonConvert.DeserializeObject<List<Workout>>(responseContent);
                    }
                    else
                    {
                        // Handle the API call failure, return an appropriate view or error message
                        return View("Error");
                    }
                }
                else
                {
                    // Handle the case when the response is null
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                // Handle any exception that occurs during the API call
                // Log the exception or return an appropriate view or error message
                return View("Error");
            }
            finally
            {
                // Dispose of the httpClient object
                httpClient?.Dispose();
            }

            // Pass the list of workouts to the view
            return View(workouts);
        }
    }
}

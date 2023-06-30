using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WelnessWebsite.Data;
using WelnessWebsite.Models;
using Microsoft.AspNetCore.Http;


namespace WelnessWebsite.Controllers
{
    public class DailyWorkoutsController : Controller
    {
        private readonly WelnessWebsiteContext _context;

        public DailyWorkoutsController(WelnessWebsiteContext context)
        {
            _context = context;
        }

        // GET: DailyWorkouts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                int userId = HttpContext.Session.GetInt32("UserId").Value;
                ViewBag.ID = HttpContext.Session.GetInt32("UserId");

                var dailyWorkouts = await _context.DailyWorkout
                    .Where(d => d.UserID == userId)
                    .ToListAsync();

                return View(dailyWorkouts);
            }
            else
            {
                // Handle the case when the UserId is not available in the session
                return Problem("User session not found");
            }
        }

        // GET: DailyWorkouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DailyWorkout == null)
            {
                return NotFound();
            }

            var dailyWorkout = await _context.DailyWorkout
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dailyWorkout == null)
            {
                return NotFound();
            }

            return View(dailyWorkout);
        }

        // GET: DailyWorkouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DailyWorkouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateTime")] DailyWorkout dailyWorkout)
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {

                // Retrieve the user ID from the session
                var userId = HttpContext.Session.GetInt32("UserId");

                // Assign the user ID to the daily workout
                dailyWorkout.UserID = HttpContext.Session.GetInt32("UserId").Value;


                _context.Add(dailyWorkout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyWorkout);
        }

        // GET: DailyWorkouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DailyWorkout == null)
            {
                return NotFound();
            }

            var dailyWorkout = await _context.DailyWorkout.FindAsync(id);
            if (dailyWorkout == null)
            {
                return NotFound();
            }
            return View(dailyWorkout);
        }

        // POST: DailyWorkouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,ID,DateTime")] DailyWorkout dailyWorkout)
        {
            if (id != dailyWorkout.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyWorkout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyWorkoutExists(dailyWorkout.ID))
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
            return View(dailyWorkout);
        }

        // GET: DailyWorkouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DailyWorkout == null)
            {
                return NotFound();
            }

            var dailyWorkout = await _context.DailyWorkout
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dailyWorkout == null)
            {
                return NotFound();
            }

            return View(dailyWorkout);
        }

        // POST: DailyWorkouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DailyWorkout == null)
            {
                return Problem("Entity set 'WelnessWebsiteContext.DailyWorkout'  is null.");
            }
            var dailyWorkout = await _context.DailyWorkout.FindAsync(id);
            if (dailyWorkout != null)
            {
                _context.DailyWorkout.Remove(dailyWorkout);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyWorkoutExists(int id)
        {
          return (_context.DailyWorkout?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

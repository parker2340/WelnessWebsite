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
        public IActionResult ProcessItem(Workout workout)
        {

            return RedirectToAction("Index", "DailyWorkouts");
        }
    }
}
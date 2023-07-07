using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WelnessWebsite.Data;
using WelnessWebsite.Models;

namespace WelnessWebsite.Controllers
{
    public class NutritionsController : Controller
    {
        private readonly WelnessWebsiteContext _context;

        public NutritionsController(WelnessWebsiteContext context)
        {
            _context = context;
        }
        // GET: Nutritions/Search
        public IActionResult Search(int ID)
        {

            HttpContext.Session.SetInt32("DailyNutritionID", ID);

            return View();
        }
        // POST: Nutritions/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ModelState.AddModelError("Nutritions", "The Name field is required.");
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
                    List<Nutrition> Nutrition = JsonConvert.DeserializeObject<List<Nutrition>>(responseContent);

                    return View("Search", Nutrition);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while searching for Nutrition. Please try again later.");
                    return View();
                }
            }
        }
        [HttpPost]
        public IActionResult SaveNutrition(string name, double calories, double servingSize, double fatTotal, double fatDouble, double protein, double sodium, double Potassium, double Cholesterol, double Carbohydrates, double fiber, double sugar)
        {
            var DNuttID = HttpContext.Session.GetInt32("DailyNutritionID").Value;

            if (ModelState.IsValid)
            {
                // Create a new instance of Nutrition and set its properties
                var newNutrition = new Nutrition
                {
                    Name = name,
                    DailyNutritionID = DNuttID,
                    Calories = calories,
                    serving_size_g = servingSize,
                    fat_total_g = fatTotal,
                    fat_saturated_g = fatDouble,
                    protein_g = protein,
                    sodium_mg = sodium,
                    potassium_mg = Potassium,
                    cholesterol_mg = Cholesterol,
                    carbohydrates_total_g = Carbohydrates,
                    fiber_g = fiber,
                    sugar_g = sugar
                };

                // Save the new nutrition to the database
                _context.Nutrition.Add(newNutrition);
                _context.SaveChanges();

                return RedirectToAction("Index", "DailyNutritions");
            }

            return BadRequest(ModelState);
        }
    }
}

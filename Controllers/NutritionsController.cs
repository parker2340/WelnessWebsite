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

                var dailyNutrition = _context.DailyNutrition.Include(dn => dn.Nutrition)
            .FirstOrDefault(dn => dn.ID == DNuttID);

                if (dailyNutrition != null)
                {
                    dailyNutrition.Calories = dailyNutrition.Nutrition.Sum(n => n.Calories);
                    dailyNutrition.serving_size_g = dailyNutrition.Nutrition.Sum(n => n.serving_size_g);
                    dailyNutrition.fat_total_g = dailyNutrition.Nutrition.Sum(n => n.fat_total_g);
                    dailyNutrition.fat_saturated_g = dailyNutrition.Nutrition.Sum(n => n.fat_saturated_g);
                    dailyNutrition.protein_g = dailyNutrition.Nutrition.Sum(n => n.protein_g);
                    dailyNutrition.sodium_mg = dailyNutrition.Nutrition.Sum(n => n.sodium_mg);
                    dailyNutrition.potassium_mg = dailyNutrition.Nutrition.Sum(n => n.potassium_mg);
                    dailyNutrition.cholesterol_mg = dailyNutrition.Nutrition.Sum(n => n.cholesterol_mg);
                    dailyNutrition.carbohydrates_total_g = dailyNutrition.Nutrition.Sum(n => n.carbohydrates_total_g);
                    dailyNutrition.fiber_g = dailyNutrition.Nutrition.Sum(n => n.fiber_g);
                    dailyNutrition.sugar_g = dailyNutrition.Nutrition.Sum(n => n.sugar_g);

                    // Retrieve the associated WeeklyNutrition
                    var weeklyNutrition = _context.WeeklyNutrition
                        .Where(u => u.ID == dailyNutrition.WeeklyNutritionID)
                        .Include(wn => wn.DailyNutrition)
                        .FirstOrDefault();

                    if (weeklyNutrition != null)
                    {
                        // Update the values in the associated WeeklyNutrition
                        weeklyNutrition.Calories = weeklyNutrition.DailyNutrition.Sum(dn => dn.Calories);
                        weeklyNutrition.serving_size_g = weeklyNutrition.DailyNutrition.Sum(dn => dn.serving_size_g);
                        weeklyNutrition.fat_total_g = weeklyNutrition.DailyNutrition.Sum(dn => dn.fat_total_g);
                        weeklyNutrition.fat_saturated_g = weeklyNutrition.DailyNutrition.Sum(dn => dn.fat_saturated_g);
                        weeklyNutrition.protein_g = weeklyNutrition.DailyNutrition.Sum(dn => dn.protein_g);
                        weeklyNutrition.sodium_mg = weeklyNutrition.DailyNutrition.Sum(dn => dn.sodium_mg);
                        weeklyNutrition.potassium_mg = weeklyNutrition.DailyNutrition.Sum(dn => dn.potassium_mg);
                        weeklyNutrition.cholesterol_mg = weeklyNutrition.DailyNutrition.Sum(dn => dn.cholesterol_mg);
                        weeklyNutrition.carbohydrates_total_g = weeklyNutrition.DailyNutrition.Sum(dn => dn.carbohydrates_total_g);
                        weeklyNutrition.fiber_g = weeklyNutrition.DailyNutrition.Sum(dn => dn.fiber_g);
                        weeklyNutrition.sugar_g = weeklyNutrition.DailyNutrition.Sum(dn => dn.sugar_g);
                    }
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "DailyNutritions");
            }

            return BadRequest(ModelState);
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelnessWebsite.Models
{
    public class DailyNutrition
    {
        public DailyNutrition()
        {
            
        }
        public int WeeklyID { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = " ID")]
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Calories")]
        public double Calories { get; set; }

        [Display(Name = "Serving Size (g)")]
        public double serving_size_g { get; set; }

        [Display(Name = "Total Fat (g)")]
        public double fat_total_g { get; set; }

        [Display(Name = " Saturated Fat (g)")]
        public double fat_saturated_g { get; set; }

        [Display(Name = " Protein (g)")]
        public double protein_g { get; set; }

        [Display(Name = " Sodium (mg)")]
        public double sodium_mg { get; set; }

        [Display(Name = " Potassium (mg)")]
        public double potassium_mg { get; set; }

        [Display(Name = " Cholesterol (mg)")]
        public double cholesterol_mg { get; set; }

        [Display(Name = "Carbohydrates (g)")]
        public double carbohydrates_total_g { get; set; }

        [Display(Name = " Fiber (g)")]
        public double fiber_g { get; set; }

        [Display(Name = " Sugar (g)")]
        public double sugar_g { get; set; }

        [Display(Name = " Date")]
        public DateTime DateTime { get; set; }
    }
}

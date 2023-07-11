using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelnessWebsite.Models
{
    public class WeeklyNutrition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Name")]
        public string? Name { get; set; }

        [Display(Name = "Calories")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double Calories { get; set; }

        [Display(Name = "Serving Size (g)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double serving_size_g { get; set; }

        [Display(Name = "Total Fat (g)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double fat_total_g { get; set; }

        [Display(Name = " Saturated Fat (g)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double fat_saturated_g { get; set; }

        [Display(Name = " Protein (g)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double protein_g { get; set; }

        [Display(Name = " Sodium (mg)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double sodium_mg { get; set; }

        [Display(Name = " Potassium (mg)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double potassium_mg { get; set; }

        [Display(Name = " Cholesterol (mg)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double cholesterol_mg { get; set; }

        [Display(Name = "Carbohydrates (g)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double carbohydrates_total_g { get; set; }

        [Display(Name = " Fiber (g)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double fiber_g { get; set; }

        [Display(Name = " Sugar (g)")]
        [DisplayFormat(DataFormatString = "{0:N2}")]

        public double sugar_g { get; set; }
        public int WeekNumber { get; set; }
        public int Year { get; set; }
        public List<DailyNutrition> DailyNutrition { get; set; }
    }
}

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
        public int ID { get; set; }
        public string? Name { get; set; }
        public double Calories { get; set; }
        public double serving_size_g { get; set; }
        public double fat_total_g { get; set;}
        public double fat_saturated_g { get; set; }
        public double protein_g { get; set; }
        public double sodium_mg { get; set; }
        public double potassium_mg { get; set; }
        public double cholesterol_mg { get; set; }
        public double carbohydrates_total_g { get; set; }
        public double fiber_g { get; set; }
        public double sugar_g { get; set; }


        public DateTime? DateTime { get; set; }
    }
}

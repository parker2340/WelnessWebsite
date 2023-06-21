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
        public int Calories { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set;}
        public int Protein { get; set; }
        public int Diber { get; set; }
        public DateTime DateTime { get; set; }
    }
}

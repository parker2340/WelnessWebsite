namespace WelnessWebsite.Models
{
    public class DailyNutrition
    {
        public DailyNutrition()
        {
            
        }
        public int WeeklyID { get; set; } 
        public int Id { get; set; }
        public int Calories { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set;}
        public int Protein { get; set; }
        public int Diber { get; set; }
        public DateTime DateTime { get; set; }
    }
}

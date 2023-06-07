namespace WelnessWebsite.Models
{
    public class WeeklyNutrition
    {
        public WeeklyNutrition()
        {
            
        }
        public int WeeklyID { get; set; }
        public int Id { get; set; }
        public int Calories { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Protein { get; set; }
        public int Fiber { get; set; }
        public List<DailyNutrition> DailyNutritions { get; set; }
    }
}

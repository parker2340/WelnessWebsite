namespace WelnessWebsite.Models
{
    public class User
    {

        public User()
        {
            
        }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? EmailConfirmed { get; set; }

        public WeeklyNutrition? WeeklyNutrition { get; set;}
        public DailyWorkout? Workout { get; set; }
    }
}

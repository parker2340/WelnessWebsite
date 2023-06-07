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
        public string? EmailConfirmed { get; set; }

        public WeeklyNutrition? WeeklyNutrition { get; set;}
        public WeeklyWorkout? Workout { get; set; }
    }
}

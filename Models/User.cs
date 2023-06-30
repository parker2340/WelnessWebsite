using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelnessWebsite.Models
{
    public class User
    {

        public User()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Password { get; set; }

         
        public WeeklyNutrition? WeeklyNutrition { get; set;}
        public DailyWorkout? Workout { get; set; }
    }
}

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
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public List<DailyWorkout> DailyWorkout { get; set; }
        public List<WeeklyNutrition> WeeklyNutrition { get; set; }
    }
}

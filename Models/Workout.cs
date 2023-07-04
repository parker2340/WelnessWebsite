using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WelnessWebsite.Models
{
    public class Workout
    {
        public Workout()
        {

        }
        [JsonIgnore]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int ID { get; set; }
        [JsonIgnore]
        public int DailyWorkoutID { get; set; }

        public string Name { get; set; }
        public string? Type { get; set; }
        public string Muscle { get; set; }
        public string Difficulty { get; set; }
        public string Instructions { get; set; }

      
    }
}

using System;

namespace WelnessWebsite.Models
{
    public class DailyWorkout
    {
        public DailyWorkout()
        {
            
        }
        public int UserID { get; set; }
        public int ID { get; set; }
        public List<Workout> WorkoutList { get; set; }
        public DateTime DateTime { get; set; }

    }
}

using System;

namespace WelnessWebsite.Models
{
    public class WeeklyWorkout
    {
        public WeeklyWorkout()
        {
            
        }
        public int ID { get; set; }
        public int UserID { get; set; }
        public List<DailyWorkout> WorkoutList { get; set; }
    }
}

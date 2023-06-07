namespace WelnessWebsite.Models
{
    public class Workout
    {
        public Workout()
        {
            
        }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MuscleGroup { get; set; }
        public string FocusedMuscle { get; set; }
    }
}

using Newtonsoft.Json;

namespace WelnessWebsite.Models
{
    public class Workout
    {
        public Workout()
        {

        }
        public int ID { get; set; }
        public int UserID { get; set; }

        public string type { get; set; }
        public string muscle { get; set; }
        public string dificulty { get; set; }
        public string instructions { get; set; }

      
    }
}

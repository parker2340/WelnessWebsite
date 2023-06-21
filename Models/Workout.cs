using Newtonsoft.Json;

namespace WelnessWebsite.Models
{
    public class Workout
    {
        public Workout()
        {

        }
        [JsonIgnore]
        public int ID { get; set; }
        [JsonIgnore]
        public int UserID { get; set; }

        public string Name { get; set; }
        public string type { get; set; }
        public string muscle { get; set; }
        public string dificulty { get; set; }
        public string instructions { get; set; }

      
    }
}

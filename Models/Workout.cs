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
        public string Type { get; set; }
        public string Muscle { get; set; }
        public string Difficulty { get; set; }
        public string Instructions { get; set; }

      
    }
}

using Newtonsoft.Json;

namespace DataLayer.Utils.SeedModels
{
    public class StateSeed
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("state_code")]
        public string Code { get; set; }
        public string CreatedBy { get; set; } = "system";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

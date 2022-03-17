using Newtonsoft.Json;

namespace DataLayer.Utils.SeedModels
{
    public class CountrySeed
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iso2")]
        public string Alpha2Code { get; set; }

        [JsonProperty("iso3")]
        public string Alpha3Code { get; set; }

        [JsonProperty("numeric_code")]
        public string NumberCode { get; set; }

        [JsonProperty("states")]
        public List<StateSeed> States { get; set; }
        public string CreatedBy { get; set; } = "system";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

using Newtonsoft.Json;

namespace WorkerService.Models
{
    public class EpisodesModel
    {
        public int ShowId { get; set; }
        public int Season { get; set; }
        [JsonProperty("espisode")]
        public int number { get; set; }
        public string Name { get; set; }
        [JsonProperty("air_date")]
        public string AirDate { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkerService.Models
{
    public class TvShowModel
    {
        public int Id { get; set; }

        [JsonProperty("id")]
        public int ReferenceId { get; set; }
        public string Name { get; set; }
        public string Permalink { get; set; }
        [JsonProperty("start_date")]
        public string StartDate { get; set; }
        [JsonProperty("end_date")]
        public string EndDate { get; set; }
        public string Country { get; set; }
        public string Network { get; set; }
        public string Status { get; set; }
        [JsonProperty("image_thumbnail_path")]
        public string ImagePath { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        [JsonProperty("description_source")]
        public string DescriptionSource { get; set; }
        public int Runtime { get; set; }
        public List<string> Genres { get; set; }
        [NotMapped]
        public List<EpisodesModel> Episodes { get; set; }
    }


    public class tvShowDetails
    {
        [JsonProperty("tvShow")]
        public TvShowModel TvShow { get; set; }
    }
}

using Newtonsoft.Json;
using System.Collections.Generic;

namespace TvShowWorkerService.Models
{
    public class TvShowModel
    {
        public int Id { get; set; }
        public string ReferenceId { get; set; }
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
        public string Image { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        [JsonProperty("description_source")]
        public string DescriptionSource { get; set; }
        public int Runtime { get; set; }
        [JsonProperty("image_thumbnail_path")]
        public string ImageThumbnailPath { get; set; }
        public List<string> Genres { get; set; }
        public List<EpisodesModel> Episodes { get; set; }
    }
}

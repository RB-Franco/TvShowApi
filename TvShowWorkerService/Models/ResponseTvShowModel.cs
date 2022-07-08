using Newtonsoft.Json;
using System.Collections.Generic;

namespace TvShowWorkerService.Models
{
    public class ResponseTvShowModel
    {
        public string Total { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }        
        [JsonProperty("tv_Shows")]
        public List<TvShowModel> TvShows { get; set; }
    }
}

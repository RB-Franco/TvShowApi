using System;
using System.Collections.Generic;

namespace Models.Models
{
    public class TvShowModel
    {
        public int Id { get; set; }
        public int ReferenceId { get; set; }
        public string Name { get; set; }
        public string Permalink { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Country { get; set; }
        public string Network { get; set; }
        public string Status { get; set; }
        public string ImagePath { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string DescriptionSource { get; set; }
        public int Runtime { get; set; }
        public string Genres { get; set; }
        public DateTime CreateDate { get; set; }
        public List<EpisodeModel> Episodes { get; set; }
    }
}

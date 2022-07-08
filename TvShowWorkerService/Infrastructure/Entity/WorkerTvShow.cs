using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TvShowWorkerService.Models;

namespace TvShowWorkerService.Infrastructure.Entity
{
    [Table("TB_TVSHOW")]
    public class WorkerTvShow
    {
        public int Id { get; set; }
        
        [Column("TS_ReferenceId")]
        public string ReferenceId { get; set; }
        
        [Column("TS_Name")]
        public string Name { get; set; }
        
        [Column("TS_Permalink")]
        public string Permalink { get; set; }
        
        [Column("TS_StartDate")]
        public string StartDate { get; set; }
        
        [Column("TS_EndDate")]
        public string EndDate { get; set; }
        
        [Column("TS_Country")]
        public string Country { get; set; }
        
        [Column("TS_Network")]
        public string Network { get; set; }
        
        [Column("TS_Status")]
        public string Status { get; set; }
        
        [Column("TS_Image")]
        public string Image { get; set; }

        [Column("TS_CreateDate")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public List<EpisodesModel> Episodes { get; set; }
    }
}

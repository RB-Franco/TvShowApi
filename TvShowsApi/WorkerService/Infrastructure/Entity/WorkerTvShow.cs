using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkerService.Models;

namespace WorkerService.Infrastructure.Entity
{
    [Table("TB_TVSHOW")]
    public class WorkerTvShow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }

        [Column("TS_ReferenceId")]
        public int ReferenceId { get; set; }

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
        public string ImagePath { get; set; }

        [Column("TS_Url")]
        public string Url { get; set; }

        [Column("TS_Description")]
        public string Description { get; set; }

        [Column("TS_Description_source")]
        public string DescriptionSource { get; set; }

        [Column("TS_Runtime")]
        public int Runtime { get; set; }

        [Column("TS_Genres")]
        public string Genres { get; set; }

        [Column("TS_CreateDate")]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Column("TS_Page")]
        public int Page { get; set; }

        [Column("TS_TotalPage")]
        public int TotalPage { get; set; }

        [NotMapped]
        public List<EpisodesModel> Episodes { get; set; } = new List<EpisodesModel>();
    }
}

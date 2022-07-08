using System.ComponentModel.DataAnnotations.Schema;

namespace TvShowWorkerService.Infrastructure.Entity
{
    [Table("TB_EPISODE")]
    public class WorkerEpisode
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        [Column("TS_Season")]
        public int Season { get; set; }
        [Column("TS_Number")]
        public int Number { get; set; }
        [Column("TS_Name")]
        public string Name { get; set; }
        [Column("TS_AirDate")]
        public string AirDate { get; set; }
    }
}

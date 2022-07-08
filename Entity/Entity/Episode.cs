using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    [Table("TB_EPISODE")]
    public class Episode
    {
        public int Id { get; set; }
        [Column("TS_Season")]
        public int Season { get; set; }
        [Column("TS_Number")]
        public int Number { get; set; }
        [Column("TS_Name")]
        public string Name { get; set; }
        [Column("TS_AirDate")]
        public string AirDate { get; set; }

        [ForeignKey("TvShow")]
        [Column(Order = 1)]
        public int ShowId { get; set; }
        public virtual TvShow TvShow { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    [Table("TB_TVSHOW")]
    public class TvShow
    {
        [Column("TVSHOW_ID")]
        public int Id { get; set; }

        [Column("TVSHOW_NAME")]
        [MaxLength(255)]
        public string Name { get; set; }

        [Column("TVSHOW_DESCRIPTION")]
        public string Description { get; set; }
    }
}

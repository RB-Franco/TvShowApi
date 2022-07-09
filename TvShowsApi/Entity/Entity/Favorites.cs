using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    [Table("TB_FAVORITES")]
    public class Favorites
    {
        public int Id { get; set; }

        [ForeignKey("TvShow")]
        [Column(Order = 1)]
        public int ShowId { get; set; }
        public virtual TvShow TvShow { get; set; }

        [ForeignKey("User")]
        [Column(Order = 1)]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}

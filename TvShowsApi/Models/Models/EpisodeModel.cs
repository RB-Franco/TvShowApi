namespace Models.Models
{
    public class EpisodeModel
    {
        public int Id { get; set; }
        public int Season { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string AirDate { get; set; }
        public int ShowId { get; set; }
    }
}

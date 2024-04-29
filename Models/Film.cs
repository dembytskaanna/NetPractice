namespace Cinema.Models
{
    public class Film
    {
        public required int FilmId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public string Genre { get; set; }
        public float? Rating { get; set; }
        public string? Trailer { get; set; }
        public string? Cast { get; set; }
    }
}

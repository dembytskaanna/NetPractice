namespace Cinema.Models
{
    public class Screening
    {
        public required int ScreeningId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        public decimal TicketPrice { get; set; }
    }
}

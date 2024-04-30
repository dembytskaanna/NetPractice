namespace Cinema.Models
{
    public class Booking
    {
        public required int BookingId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public User UserId { get; set; }
        public Screening ScreeningId { get; set; }
        public int TicketCount { get; set; }
    }
}

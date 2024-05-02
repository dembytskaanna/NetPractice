namespace Cinema.Models
{
    public class Booking
    {
        public required int BookingId { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ScreeningId { get; set; }
        public int TicketCount { get; set; }

        public User User { get; set; }
        public Screening Screening { get; set; }
    }
}

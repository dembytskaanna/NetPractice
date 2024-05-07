namespace Cinema.Dto
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public string Status { get; set; } = "BOOKED"; // Default value "BOOKED
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int ScreeningId { get; set; }
        public int TicketCount { get; set; }
    }
}

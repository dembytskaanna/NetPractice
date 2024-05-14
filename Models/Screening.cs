using Microsoft.EntityFrameworkCore;

namespace Cinema.Models
{
    public class Screening
    {
        public int ScreeningId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        [Precision(8, 2)]
        public decimal TicketPrice { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        
        public ICollection<Booking>? Bookings { get; set; }
        public Film Film { get; set; }
        public Hall Hall { get; set; }
    }
}

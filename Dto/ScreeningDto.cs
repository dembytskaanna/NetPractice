using Microsoft.EntityFrameworkCore;

namespace Cinema.Dto
{
    public class ScreeningDto
    {
        public int ScreeningId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        [Precision(8, 2)]
        public decimal TicketPrice { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
    }
}

﻿namespace Cinema.Models
{
    public class Screening
    {
        public required int ScreeningId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        public decimal TicketPrice { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        
        public ICollection<Booking> Boookings { get; set; }
        public Film Film { get; set; }
        public Hall Hall { get; set; }
    }
}

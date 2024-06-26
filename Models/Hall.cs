﻿namespace Cinema.Models
{
    public class Hall
    {
        public int HallId { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public ICollection<Screening>? Screenings { get; set; }
    }
}

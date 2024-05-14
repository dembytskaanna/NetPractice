namespace Cinema.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? HashedPassword { get; set; }
        public bool? IsAdmin { get; set; } = false; // default value
        public ICollection<Booking>? Bookings { get; set; }
    }
}

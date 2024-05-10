using Cinema.Data;
using Cinema.Interfaces;
using Cinema.Models;

namespace Cinema.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;

        public BookingRepository(DataContext context)
        {
            _context = context;
        }

        public bool BookingExists(int bookingId)
        {
            return _context.Bookings.Any(b => b.BookingId == bookingId);
        }

        public ICollection<Booking> GetBookings()
        {
            return _context.Bookings.OrderBy(f => f.BookingId).ToList();
        }

        public Booking GetBooking(int bookingId)
        {
            return _context.Bookings.Where(b => b.BookingId == bookingId).FirstOrDefault();
        }  

        public User GetUserByBooking(int bookingId)
        {
            return _context.Users.Where(u => u.Bookings.Any(b => b.BookingId == bookingId)).FirstOrDefault();
        }

        public Screening GetScreeningByBooking(int bookingId)
        {
            return _context.Screenings.Where(s => s.Boookings.Any(b => b.BookingId == bookingId)).FirstOrDefault();
        }
        public bool CreateBooking(Booking booking)
        {
            _context.Add(booking);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IBookingRepository
    {
        ICollection<Booking> GetBookings();
        Booking GetBooking(int bookingId);
        User GetUserByBooking(int bookingId);
        Screening GetScreeningByBooking(int bookingId);
        bool BookingExists(int bookingId);
    }
}

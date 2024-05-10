using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IScreeningRepository 
    {
        ICollection<Screening> GetScreenings();
        Screening GetScreening(int screeningId);
        ICollection <Booking> GetBookingsByScreening (int screeningId);
        bool ScreeningExists(int screeningId);
        bool CreateScreening(Screening screening);
        bool UpdateScreening(Screening screening);
        bool Save();
    }
}

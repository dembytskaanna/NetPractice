using System;
using Cinema.Data;
using Cinema.Interfaces;
using Cinema.Models;

namespace Cinema.Repository
{
    public class ScreeningRepository : IScreeningRepository
    {
        private readonly DataContext _context;

        public ScreeningRepository(DataContext context) 
        { 
            _context = context; 
        }

        public ICollection<Screening> GetScreenings()
        {
            return _context.Screenings.OrderBy(s => s.ScreeningId).ToList();
        }

        public Screening GetScreening(int screeningId)
        {
            return _context.Screenings.Where(s => s.ScreeningId == screeningId).FirstOrDefault();
        } 
        
        public ICollection<Booking> GetBookingsByScreening(int screeningId)
        {
            return _context.Bookings.Where(b => b.ScreeningId == screeningId).ToList();
        }

        public bool ScreeningExists(int screeningId)
        {
            return _context.Screenings.Any(s => s.ScreeningId == screeningId);
        }

        public bool CreateScreening(Screening screening)
        {
            _context.Add(screening);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateScreening(Screening screening)
        {
            _context.Update(screening);
            return Save();
        }

        public bool DeleteScreening(Screening screening)
        {
            _context.Remove(screening);
            return Save();
        }
    }
}

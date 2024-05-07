using Cinema.Data;
using Cinema.Interfaces;
using Cinema.Models;

namespace Cinema.Repository
{
    public class HallRepository : IHallRepository
    {
        private readonly DataContext _context;

        public HallRepository(DataContext context) 
        {
            _context = context;
        }
        public Hall GetHall(int hallId)
        {
            return _context.Halls.Where(h => h.HallId == hallId).FirstOrDefault();
        }

        public ICollection<Hall> GetHalls()
        {
            return _context.Halls.OrderBy(h => h.HallId).ToList();
        }

        public ICollection<Screening> GetScreeningByHall(int hallId)
        {
            return _context.Screenings.Where(s => s.HallId == hallId).ToList();
        }

        public bool HallExists(int hallId)
        {
            return _context.Halls.Any(h => h.HallId == hallId);
        }
    }
}

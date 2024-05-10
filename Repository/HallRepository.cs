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

        public bool CreateHall(Hall hall)
        {
            _context.Add(hall);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateHall(Hall hall)
        {
            _context.Update(hall);
            return Save();
        }

        public bool DeleteHall(Hall hall)
        {
            _context.Remove(hall);
            return Save();
        }
    }
}

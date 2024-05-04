using Cinema.Data;
using Cinema.Interfaces;
using Cinema.Models;

namespace Cinema.Repository
{
    public class FilmRepository : IFilmRepository
    {
        private readonly DataContext _context;

        public FilmRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Film> GetFilms()
        {
            return _context.Films.OrderBy(f => f.FilmId).ToList();
        }
    }
}
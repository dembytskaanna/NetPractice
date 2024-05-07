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

        public bool FilmExists(int filmId)
        {
            return _context.Films.Any(f => f.FilmId == filmId);
        }

        public Film GetFilm(int filmId)
        {
            return _context.Films.Where(f => f.FilmId == filmId).FirstOrDefault();
        }

        public Film GetFilm(string title)
        {
            return _context.Films.Where(f => f.Title == title).FirstOrDefault();
        }

        public ICollection<Film> GetFilms()
        {
            return _context.Films.OrderBy(f => f.FilmId).ToList();
        }
        public ICollection<Screening> GetScreeningsByFilm(int filmId)
        {
            return _context.Screenings.Where(s => s.FilmId == filmId).ToList();
        }
    }
}
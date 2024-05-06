using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IFilmRepository
    {
        ICollection<Film> GetFilms();
        Film GetFilm(int filmId);
        Film GetFilm(string title);  
        bool FilmExists(int filmId);
    }
}
using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IFilmRepository
    {
        ICollection<Film> GetFilms();
        Film GetFilm(int filmId);
        Film GetFilm(string title);  
        bool FilmExists(int filmId);
        ICollection<Screening> GetScreeningsByFilm(int filmId);
        bool CreateFilm(Film film);
        bool UpdateFilm(Film film); 
        bool DeleteFilm(Film film);
        bool Save();
    }
}
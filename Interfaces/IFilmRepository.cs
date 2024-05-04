using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IFilmRepository
    {
        ICollection<Film> GetFilms();
    }
}
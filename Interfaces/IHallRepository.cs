using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IHallRepository
    {
        ICollection<Hall> GetHalls();
        Hall GetHall(int hallId);
        ICollection<Screening> GetScreeningByHall(int hallId);
        bool HallExists(int hallId);
        bool CreateHall(Hall hall);
        bool UpdateHall(Hall hall); 
        bool Save();
    }
}

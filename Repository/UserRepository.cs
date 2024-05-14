using Cinema.Data;
using Cinema.Interfaces;
using Cinema.Models;

namespace Cinema.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserId).ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId).FirstOrDefault();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.UserId == userId);
        }

        public bool Register(User user)
        {
            _context.Add(user);
            return Save();
        }

        public ICollection<Booking> GetBookingsByUser(int userId)
        {
            return _context.Bookings.Where(b => b.UserId == userId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }
    }
}

﻿using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int userId);
        ICollection<Booking> GetBookingsByUser(int userId);
        bool UserExists(int userId);
    }
}
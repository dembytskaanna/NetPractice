using AutoMapper;
using Cinema.Dto;
using Cinema.Models;

namespace Cinema.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Film, FilmDto>();
            CreateMap<FilmDto, Film>();
            CreateMap<Hall, HallDto>();
            CreateMap<HallDto, Hall>();
            CreateMap<Screening, ScreeningDto>();
            CreateMap<Booking, BookingDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}

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
        }
    }
}

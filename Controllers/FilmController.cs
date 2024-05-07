using Microsoft.AspNetCore.Mvc;
using Cinema.Interfaces;
using Cinema.Models;
using AutoMapper;
using Cinema.Dto;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMapper _mapper;

        public FilmController(IFilmRepository filmRepository, IMapper mapper)
        {
            _filmRepository = filmRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public IActionResult GetFilms()
        {
            var films = _mapper.Map<List<FilmDto>>(_filmRepository.GetFilms());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(films);
        }

        [HttpGet("{filmId}")]
        [ProducesResponseType(200, Type = typeof(Film))]
        [ProducesResponseType(400)]
        public IActionResult GetFilm(int filmId)
        {
            if (!_filmRepository.FilmExists(filmId))
                return NotFound();

            var film = _mapper.Map<FilmDto>(_filmRepository.GetFilm(filmId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(film);
        }

        [HttpGet("screenings/{filmId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Screening>))]
        [ProducesResponseType(400)]
        public IActionResult GetScreeningsByFilm(int filmId)
        {
            if (!_filmRepository.FilmExists(filmId))
                return NotFound();
            var screenings = _mapper.Map<List<ScreeningDto>>(_filmRepository.GetScreeningsByFilm(filmId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(screenings);
        }
    }
}
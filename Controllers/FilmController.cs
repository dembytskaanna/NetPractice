using Microsoft.AspNetCore.Mvc;
using Cinema.Interfaces;
using Cinema.Models;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;

        public FilmController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Film>))]
        public IActionResult GetFilms()
        {
            var films = _filmRepository.GetFilms();

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

            var film = _filmRepository.GetFilm(filmId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(film);
        }

    }
}
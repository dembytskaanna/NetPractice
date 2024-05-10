using Microsoft.AspNetCore.Mvc;
using Cinema.Interfaces;
using Cinema.Models;
using AutoMapper;
using Cinema.Dto;
using Cinema.Repository;

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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateFilm([FromBody] FilmDto filmCreate)
        {
            if (filmCreate == null)
                return BadRequest(ModelState);

            var film = _filmRepository.GetFilms()
                .Where(f => f.Title.Trim().ToUpper() == filmCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (film != null)
            {
                ModelState.AddModelError("", "Film alredy exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filmMap = _mapper.Map<Film>(filmCreate);

            if (!_filmRepository.CreateFilm(filmMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{filmId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateFilm(int filmId, [FromBody] FilmDto updatedFilm)
        {
            if (updatedFilm == null)
                return BadRequest(ModelState);

            if (filmId != updatedFilm.FilmId)
                return BadRequest(ModelState);

            if (!_filmRepository.FilmExists(filmId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var filmMap = _mapper.Map<Film>(updatedFilm);

            if (!_filmRepository.UpdateFilm(filmMap))
            {
                ModelState.AddModelError("", "Something went wrong updating booking");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
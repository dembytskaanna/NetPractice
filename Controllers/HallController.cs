using AutoMapper;
using Cinema.Dto;
using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IHallRepository _hallRepository;
        private readonly IMapper _mapper;

        public HallController(IHallRepository hallRepository, IMapper mapper)
        {
            _hallRepository = hallRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Hall>))]
        public IActionResult GetHalls()
        {
            var halls = _mapper.Map<List<HallDto>>(_hallRepository.GetHalls());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(halls);
        }

        [HttpGet("{hallId}")]
        [ProducesResponseType(200, Type = typeof(Hall))]
        [ProducesResponseType(400)]
        public IActionResult GetHall(int hallId)
        {
            if (!_hallRepository.HallExists(hallId))
                return NotFound();

            var hall = _mapper.Map<HallDto>(_hallRepository.GetHall(hallId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(hall);
        }

        [HttpGet("screenings/{hallId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Screening>))]
        [ProducesResponseType(400)]
        public IActionResult GetScreeningByHall(int hallId)
        {
            if (!_hallRepository.HallExists(hallId))
                return NotFound();

            var screenings = _mapper.Map<List<ScreeningDto>>(_hallRepository.GetScreeningByHall(hallId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(screenings);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateHall([FromBody] HallDto hallCreate)
        {
            if (hallCreate == null)
                return BadRequest(ModelState);

            var hall = _hallRepository.GetHalls()
                .Where(h => h.Name.Trim().ToUpper() == hallCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (hall != null)
            {
                ModelState.AddModelError("", "Hall alredy exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hallMap = _mapper.Map<Hall>(hallCreate);

            if (!_hallRepository.CreateHall(hallMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{hallId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateHall(int hallId, [FromBody] HallDto updatedHall)
        {
            if (updatedHall == null)
                return BadRequest(ModelState);

            if (hallId != updatedHall.HallId)
                return BadRequest(ModelState);

            if (!_hallRepository.HallExists(hallId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hallMap = _mapper.Map<Hall>(updatedHall);

            if (!_hallRepository.UpdateHall(hallMap))
            {
                ModelState.AddModelError("", "Something went wrong updating booking");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        [HttpDelete("{hallId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteFilm(int hallId)
        {
            if (!_hallRepository.HallExists(hallId))
            {
                return NotFound();
            }

            var hallToDelete = _hallRepository.GetHall(hallId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_hallRepository.DeleteHall(hallToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting booking");
            }

            return NoContent();
        }
    }
}

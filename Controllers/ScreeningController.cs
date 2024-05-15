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
    public class ScreeningController : ControllerBase
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IMapper _mapper;

        public ScreeningController(IScreeningRepository screeningRepository, IMapper mapper)
        {
            _screeningRepository = screeningRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Screening>))]
        public IActionResult GetScreenings()
        {
            var screenings = _mapper.Map<List<ScreeningDto>>(_screeningRepository.GetScreenings());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(screenings);
        }

        [HttpGet("{screeningId}")]
        [ProducesResponseType(200, Type = typeof(Screening))]
        [ProducesResponseType(400)]
        public IActionResult GetScreening(int screeningId)
        {
            if (!_screeningRepository.ScreeningExists(screeningId))
                return NotFound();

            var screening = _mapper.Map<ScreeningDto>(_screeningRepository.GetScreening(screeningId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(screening);
        }

        [HttpGet("bookings/{screeningId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookingsByScreening (int screeningId)
        {
            if (!_screeningRepository.ScreeningExists(screeningId))
                return NotFound();

            var bookings = _mapper.Map<List<BookingDto>>(_screeningRepository.GetBookingsByScreening(screeningId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }

        [HttpGet("date/{date}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Screening>))]
        [ProducesResponseType(400)]
        public IActionResult GetScreeningsByDate(DateTime date)
        {
            if (!_screeningRepository.GetScreeningsByDate(date).Any())
                return NotFound();

            var screenings = _mapper.Map<List<ScreeningDto>>(_screeningRepository.GetScreeningsByDate(date));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(screenings);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateScreening([FromBody] ScreeningDto screeningCreate)
        {
            if (screeningCreate == null)
                return BadRequest(ModelState);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var screeningMap = _mapper.Map<Screening>(screeningCreate);


            if (!_screeningRepository.CreateScreening(screeningMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{screeningId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateScreening(int screeningId, [FromBody] ScreeningDto updatedScreening)
        {
            if (updatedScreening == null)
                return BadRequest(ModelState);

            if (screeningId != updatedScreening.ScreeningId)
                return BadRequest(ModelState);

            if (!_screeningRepository.ScreeningExists(screeningId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var screeningMap = _mapper.Map<Screening>(updatedScreening);

            if (!_screeningRepository.UpdateScreening(screeningMap))
            {
                ModelState.AddModelError("", "Something went wrong updating booking");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{screeningId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteScreening(int screeningId)
        {
            if (!_screeningRepository.ScreeningExists(screeningId))
            {
                return NotFound();
            }

            var screeningToDelete = _screeningRepository.GetScreening(screeningId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_screeningRepository.DeleteScreening(screeningToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting booking");
            }

            return NoContent();
        }

    }
}

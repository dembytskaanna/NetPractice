using Microsoft.AspNetCore.Mvc;
using Cinema.Interfaces;
using Cinema.Models;
using AutoMapper;
using Cinema.Dto;
using Cinema.Repository;
using Microsoft.AspNetCore.Authorization;
using Cinema.Services;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper, IUserService userService)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        public IActionResult GetBookings()
        {
            var bookings = _mapper.Map<List<BookingDto>>(_bookingRepository.GetBookings());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }

        [HttpGet("{bookingId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        public IActionResult GetBooking(int bookingId)
        {
            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            var booking = _mapper.Map<BookingDto>(_bookingRepository.GetBooking(bookingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string role = _userService.GetMyRole();
            if (role == "Admin") return Ok(booking);

            int userId = _userService.GetMyId();
            if (booking.UserId == userId) return Ok(booking);

            return Unauthorized();
        }

        [HttpGet("users/{bookingId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByBooking(int bookingId) {

            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            var user = _mapper.Map<UserDto>(_bookingRepository.GetUserByBooking(bookingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string role = _userService.GetMyRole();
            if (role == "Admin") return Ok(user);
            int userId = _userService.GetMyId();
            if (user.UserId == userId) return Ok(user);

            return Unauthorized();
        }

        [HttpGet("screenings/{bookingId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(Screening))]
        [ProducesResponseType(400)]
        public IActionResult GetScreeningByBooking(int bookingId)
        {

            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            var screening = _mapper.Map<ScreeningDto>(_bookingRepository.GetScreeningByBooking(bookingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(screening);
        }

        [HttpPost, Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateBooking([FromQuery] int userId, [FromQuery] int screeningId, [FromBody] BookingDto bookingCreate)
        {
            if (bookingCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookingMap = _mapper.Map<Booking>(bookingCreate);

            bookingMap.UserId = userId;
            bookingMap.ScreeningId = screeningId;

            int myId = _userService.GetMyId();
            string role = _userService.GetMyRole();

            if (userId != myId && role != "Admin") return Unauthorized();


            if (!_bookingRepository.CreateBooking(bookingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{bookingId}"), Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBooking(int bookingId, [FromBody] BookingDto updatedBooking)
        {
            if(updatedBooking == null)
                return BadRequest(ModelState);

            if(bookingId != updatedBooking.BookingId)
                return BadRequest(ModelState);

            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookingMap = _mapper.Map<Booking>(updatedBooking);

            int myId = _userService.GetMyId();
            string role = _userService.GetMyRole();

            if (bookingMap.UserId != myId && role != "Admin") return Unauthorized();

            if(!_bookingRepository.UpdateBooking(bookingMap))
            {
                ModelState.AddModelError("", "Something went wrong updating booking");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{bookingId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBooking(int bookingId) 
        { 
            if(!_bookingRepository.BookingExists(bookingId))
            {
                return NotFound();
            }

            var bookingToDelete = _bookingRepository.GetBooking(bookingId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bookingRepository.DeleteBooking(bookingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting booking");
            }

            return NoContent();
        }

    }
}

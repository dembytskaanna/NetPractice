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
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        public IActionResult GetBookings()
        {
            var bookings = _mapper.Map<List<BookingDto>>(_bookingRepository.GetBookings());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }

        [HttpGet("{bookingId}")]
        [ProducesResponseType(200, Type = typeof(Booking))]
        [ProducesResponseType(400)]
        public IActionResult GetBooking(int bookingId)
        {
            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            var booking = _mapper.Map<BookingDto>(_bookingRepository.GetBooking(bookingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(booking);
        }

        [HttpGet("users/{bookingId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByBooking(int bookingId) {

            if (!_bookingRepository.BookingExists(bookingId))
                return NotFound();

            var user = _mapper.Map<UserDto>(_bookingRepository.GetUserByBooking(bookingId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("screenings/{bookingId}")]
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateBooking([FromBody] BookingDto bookingCreate)
        {
            if (bookingCreate == null)
                return BadRequest(ModelState);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookingMap = _mapper.Map<Booking>(bookingCreate);


            if (!_bookingRepository.CreateBooking(bookingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}

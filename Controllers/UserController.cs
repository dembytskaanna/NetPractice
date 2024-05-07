using AutoMapper;
using Cinema.Dto;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("bookings/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookingsByUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var bookings = _mapper.Map<List<BookingDto>>(_userRepository.GetBookingsByUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }
    }
}

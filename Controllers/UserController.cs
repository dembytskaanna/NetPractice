using AutoMapper;
using Cinema.Dto;
using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Repository;
using Cinema.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserRepository userRepository, IMapper mapper, IUserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet, Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{userId}"), Authorize(Roles = "Admin")]
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

        [HttpGet("bookings/{userId}"), Authorize]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Booking>))]
        [ProducesResponseType(400)]
        public IActionResult GetBookingsByUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            int myId = _userService.GetMyId();
            string myRole = _userService.GetMyRole();
            if(myId != userId && myRole != "Admin") return Unauthorized();

            var bookings = _mapper.Map<List<BookingDto>>(_userRepository.GetBookingsByUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bookings);
        }

        [HttpPut("{userId}"), Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int userId, [FromBody] UserRegistrationDto updatedUser)
        {
            

            if (updatedUser == null)
                return BadRequest(ModelState);

            if (userId != updatedUser.UserId)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            int myId = _userService.GetMyId();
            string myRole = _userService.GetMyRole();

            if (myId != userId && myId != updatedUser.UserId) return Unauthorized();

            var userMap = _mapper.Map<User>(updatedUser);

            if (!_userRepository.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating booking");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userId}"), Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            var userToDelete = _userRepository.GetUser(userId);

            if ((bool)userToDelete.IsAdmin) return BadRequest("Cannot delete admin");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_userRepository.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting booking");
            }



            return NoContent();
        }
    }
}

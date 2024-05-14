using AutoMapper;
using Cinema.Dto;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Cinema.Services;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AuthController(IUserRepository userRepository, IMapper mapper, IConfiguration config, IUserService userService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
            _userService = userService;
        }

        [HttpGet("GetMyRole"), Authorize]
        public ActionResult<string> GetMyRole() {
            var myRole = _userService.GetMyRole();
            return Ok(myRole);
        }

        [HttpGet("GetMyId"), Authorize]
        public ActionResult<string> GetMyId()
        {
            var myId = _userService.GetMyId();
            return Ok(myId);
        }

        [HttpPost("register")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult Register([FromBody] UserRegistrationDto userRegister)
        {
            if (userRegister == null)
                return BadRequest(ModelState);

            var user = _userRepository.GetUsers()
                .Where(u => u.Email.Trim().ToUpper() == userRegister.Email.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User alredy exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);
            var userMap = _mapper.Map<User>(userRegister);
            userMap.HashedPassword = passwordHash;


            if (!_userRepository.Register(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPost("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            if (userLogin == null)
                return BadRequest(ModelState);

            var user = _userRepository.GetUsers()
                .Where(u => u.Email.Trim().ToUpper() == userLogin.Email.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("", "User does not exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!BCrypt.Net.BCrypt.Verify(userLogin.Password, user.HashedPassword))
            {
                ModelState.AddModelError("", "Invalid password");
                return StatusCode(422, ModelState);
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken (User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, (bool)user.IsAdmin ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

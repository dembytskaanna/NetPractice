using AutoMapper;
using Cinema.Dto;
using Cinema.Interfaces;
using Cinema.Models;
using Cinema.Repository;
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
    }
}

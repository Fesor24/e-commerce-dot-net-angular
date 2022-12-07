using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGenericRepository<Games> _gamesRepo;
        private readonly IGenericRepository<Genre> _genreRepo;
        private readonly IGenericRepository<ConsoleDevice> _consoleRepo;
        private readonly IMapper _mapper;

        public GamesController(IGenericRepository<Games> gamesRepo, 
            IGenericRepository<Genre> genreRepo, 
            IGenericRepository<ConsoleDevice> consoleRepo,
            IMapper mapper)
        {
            _gamesRepo = gamesRepo;
            _genreRepo = genreRepo;
            _consoleRepo = consoleRepo;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GamesToReturnDto>> GetGameById(int id)
        {
            var spec = new GamesWithGenreAndConsoleSpecification(id);
            var game = await _gamesRepo.GetEntityWithSpec(spec);
            if (game is null) return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<GamesToReturnDto>(game));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GamesToReturnDto>>> GetAllGames()
        {
            var spec = new GamesWithGenreAndConsoleSpecification();
            var games = await _gamesRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Games>, IReadOnlyList<GamesToReturnDto>>(games));
        }

    }
}

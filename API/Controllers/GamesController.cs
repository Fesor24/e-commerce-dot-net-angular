using API.DTOs;
using API.Errors;
using API.Helper;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pagination<GamesToReturnDto>>> GetAllGames([FromQuery] GamesSpecParams gamesSpec)
        {
            var spec = new GamesWithGenreAndConsoleSpecification(gamesSpec);

            //In this spec, we have applied filters to our games and we are basically returning an IQueryable
            //If no filters were applied, it will return all games
            var countSpec = new GamesWithFiltersCountSpecification(gamesSpec);

            //We are applying the count async method to our countSpec
            var totalItems = await _gamesRepo.CountAsync(countSpec);

            var games = await _gamesRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<GamesToReturnDto>>(games);

            return Ok(new Pagination<GamesToReturnDto>(gamesSpec.PageIndex, gamesSpec.PageSize, totalItems, data));
        }

        [HttpGet("Genre")]
        public async Task<ActionResult<IReadOnlyList<Genre>>> GetAllGenres()
        {
            var allGenres = await _genreRepo.ListAllAsync();

            return Ok(allGenres);
        }

        [HttpGet("Console")]
        public async Task<ActionResult<IReadOnlyList<ConsoleDevice>>> GetAllDevice()
        {
            var allDevice = await _consoleRepo.ListAllAsync();
            return Ok(allDevice);
        }

    }
}

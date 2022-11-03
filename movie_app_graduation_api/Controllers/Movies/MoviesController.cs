using Microsoft.AspNetCore.Mvc;
using MoviesApi.Dtos;

namespace MoviesApi.Controllers.Movies

{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        readonly MoviesDbContext _dbContext;

        public MoviesController(MoviesDbContext applicationDbContext)
        => _dbContext = applicationDbContext;


        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            try
            {

                var movies = await _dbContext.Movies.OrderByDescending(movie => movie.Rate)
                    .Include(movie => movie.Genre).ToListAsync();
                return Ok(movies);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            try
            {

                var movie = await _dbContext.Movies.Include(movie => movie.Genre)
                    .SingleOrDefaultAsync(movie => movie.Id == id);
                if (movie == null)
                    return NotFound($"No Movie with id : {id}");
                return Ok(movie);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetMoviesByGenre")]
        public async Task<IActionResult> GetMoviesByGenre(byte id)
        {
            try
            {

                var movies = await _dbContext.Movies
                    .OrderByDescending(movie => movie.Rate).Where(m => m.GenreId == id)
                    .Include(movie => movie.Genre).ToListAsync();
                return Ok(movies);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreateMovie([FromForm] MovieDtos movieDto)
        {

            try
            {

                //create movie object
                var movie = new Movie()
                {
                    Title = movieDto.Title!,
                    Rate = movieDto.Rate,
                    Year = movieDto.Year,
                    Storeline = movieDto.Storeline!,
                    GenreId = movieDto.GenreId,
                    Genre = movieDto.Genre!,
                    Poster = movieDto.Poster
                };
                //add object  
                await _dbContext.Movies.AddAsync(movie);
                //update database
                _dbContext.SaveChanges();

                return Ok(movie);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }


    }

}

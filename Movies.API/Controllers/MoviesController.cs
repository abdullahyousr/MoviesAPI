using AutoMapper;
using Movies.BL.Interface;
using Movies.BL.Models;
using Movies.DAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRep _movie;
        private readonly IMapper _mapper;
        private readonly IGenreRep _genre;

        public MoviesController(IMovieRep movie, IMapper mapper, IGenreRep genre)
        {
            _movie = movie;
            _mapper = mapper;
            _genre = genre;
        }
        [HttpGet(template:"{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = await _movie.GetById(id);

            if(movie == null)
                return NotFound();
            return Ok(movie);
        }
        [HttpGet(template: "GetByGenreId")]
        public async Task<IActionResult> GetByGenreId(byte genreId)
        {
            var result = await _movie.GetAll(genreId);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _movie.GetAll();
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> NewMovie([FromForm] MovieVM obj)
        {
            if(obj.Poster == null)
                return BadRequest("Poster is required");

            if( !  _movie.IsAllowedExtensions(obj.Poster.FileName) )
                return BadRequest("Only .jpg and .png are allowed!");
            
            if( !  _movie.IsAllowedSize(obj.Poster.Length) )
                return BadRequest("Max allowedcd size is 1MB");
            
            if( ! await _genre.IsAllowedGenre(obj.GenreId) )
                return BadRequest("Invalid Genre ID");

            using var dataStream = new MemoryStream();
            await obj.Poster.CopyToAsync(dataStream);

            var movie = _mapper.Map<Movie>(obj);
            movie.Poster = dataStream.ToArray();

            var result = await _movie.CreateMovie(movie);
            return Ok(result);
        }

        [HttpPut(template:("{id}"))]
        public async Task<IActionResult> UpdateMovie(int id, [FromForm] MovieVM obj)
        {
            var movieFound = await _movie.GetById(id);
            if (movieFound == null)
                return NotFound($"No movie was found with ID: {id}");

            if (!await _genre.IsAllowedGenre(obj.GenreId))
                return BadRequest("Invalid Genre ID");
       
            if(obj.Poster != null)
            {
                if( !  _movie.IsAllowedExtensions(obj.Poster.FileName) )
                    return BadRequest("Only .jpg and .png are allowed!");
                if( !  _movie.IsAllowedSize(obj.Poster.Length) )
                    return BadRequest("Max allowedcd size is 1MB");
                
                using var dataStream = new MemoryStream();
                await obj.Poster.CopyToAsync(dataStream);
                
                movieFound.Poster = dataStream.ToArray();
            }

            movieFound.Title = obj.Title;
            movieFound.Rate = obj.Rate;
            movieFound.StoreLine = obj.StoreLine;
            movieFound.Year = obj.Year;
            movieFound.GenreId = obj.GenreId;

            var result = await _movie.UpdateMovie(movieFound);

            return Ok(result);
        }

        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movieFound = await _movie.GetById(id);
            if (movieFound == null)
                return NotFound($"No movie was found with ID: {id}");

            var result = await _movie.DeleteMovie(movieFound);

            return Ok(result);
        }
    }
}
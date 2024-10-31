using AutoMapper;
using Movies.BL.Interface;
using Movies.BL.Models;
using Movies.DAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRep _genre;
        private readonly IMapper _mapper;

        public GenresController(IGenreRep genre, IMapper mapper)
        {
            _genre = genre;
            _mapper = mapper;
        }

        [HttpGet]
        //[Route("~/Genres/GetAllAsync")]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genre.GetAll();
            var result = _mapper.Map<IEnumerable<GenreVM>>(genres);

            return Ok(result);
        }
        
        [HttpPost]
        //[Route("~/Genres/NewGenre")]
        public async Task<IActionResult> NewGenre(GenreVM obj)
        {
            var genre = _mapper.Map<Genre>(obj);
            var result = await _genre.CreateGenre(genre);

            return Ok(result);
        }

        [HttpPut(template:"{id}")]
        public async Task<IActionResult> Update(byte id, [FromBody] GenreVM obj)
        {
            var genreFound = await _genre.GetById(id);
            if(genreFound == null)
                return NotFound($"No genre was found with ID: {id}");

            genreFound.Name = obj.Name;
            var result = await _genre.UpdateGenre(genreFound);
            
            return Ok(result);
        }

        [HttpDelete(template:"{id}")]
        public async Task<IActionResult> Delete(byte id)
        {
            var genreFound = await _genre.GetById(id);
            if(genreFound == null)
                return NotFound($"No genre was found with ID: {id}");

            var result = await _genre.DeleteGenre(genreFound);
            return Ok(result);
        }
    }
}
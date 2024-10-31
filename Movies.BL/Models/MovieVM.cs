using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Movies.BL.Models
{
    public class MovieVM
    {
        public int Id { get; set; }

        [MaxLength(length: 250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        
        [MaxLength(length: 2500)]
        public string StoreLine { get; set; }
        public IFormFile? Poster { get; set; }
        public byte GenreId { get; set; }
    }
}
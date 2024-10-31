using System.ComponentModel.DataAnnotations;

namespace Movies.BL.Models
{
    public class GenreVM
    {
        public byte Id { get; set; }
        
        [MaxLength(length: 100)]
        public string Name { get; set; }
    }
}
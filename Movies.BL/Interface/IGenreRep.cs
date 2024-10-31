using Movies.DAL.Entity;

namespace Movies.BL.Interface
{
    public interface IGenreRep 
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(byte id);
        Task<Genre> CreateGenre(Genre genre);
        Task<Genre> UpdateGenre(Genre obj);
        Task<Genre> DeleteGenre(Genre obj);
        Task<bool> IsAllowedGenre(byte id);
    }
}
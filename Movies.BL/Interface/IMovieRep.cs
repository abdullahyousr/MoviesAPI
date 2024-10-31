using Movies.DAL.Entity;

namespace Movies.BL.Interface
{
    public interface IMovieRep 
    {
        Task<Movie> GetById(int id);
        Task<IEnumerable<Movie>> GetAll(byte genreId = 0);
        Task<Movie> CreateMovie(Movie obj);
        Task<Movie> UpdateMovie(Movie obj);
        Task<Movie> DeleteMovie(Movie obj);

        bool IsAllowedExtensions(string poster);
        bool IsAllowedSize(long size);

    }
}
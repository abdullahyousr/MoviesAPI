using Movies.BL.Interface;
using Movies.DAL.Database;
using Movies.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Movies.BL.Repository
{
    public class MovieRep : IMovieRep
    {
        private readonly ApplicationDbContext _db;
        private List<string> _allowedExtensions = new List<string> {".jpg",".png"};
        private long _maxAllowedPosterSize = 1048576;

        public MovieRep(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task<Movie> GetById(int id)
        {
            var result = await _db.Movies
                    .Include(m => m.Genre)
                    .SingleOrDefaultAsync(m => m.Id ==id);

            return result;
        }
       public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            var result = await _db.Movies
                    .Where(m => m.GenreId == genreId || genreId == 0 )
                    .OrderByDescending(m => m.Rate)
                    .Include(m => m.Genre)
                    .ToListAsync();
            return result;
        }

        public async Task<Movie> CreateMovie(Movie obj)
        {
            await _db.Movies.AddAsync(obj);
            await _db.SaveChangesAsync();

            return obj;
        }

        public async Task<Movie> UpdateMovie(Movie obj)
        {
            _db.Entry(obj).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return (obj);
        }

        public async Task<Movie> DeleteMovie(Movie obj)
        {
            _db.Movies.Remove(obj);
            await _db.SaveChangesAsync();

            return obj;
        }

        public bool IsAllowedExtensions(string fileName)
        {
            var result = _allowedExtensions.Contains(Path.GetExtension(fileName).ToLower());
            return result;
        }
        public bool IsAllowedSize(long size)
        {
            var result = _maxAllowedPosterSize >= size ;
            return result;
        }


    }
}

using Movies.BL.Interface;
using Movies.DAL.Database;
using Movies.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace Movies.BL.Repository
{
    public class GenreRep : IGenreRep
    {
        private readonly ApplicationDbContext _db;

        public GenreRep(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<Genre> CreateGenre(Genre obj)
        {
            await _db.Genres.AddAsync(obj);
            await _db.SaveChangesAsync();

            return obj;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genres = await _db.Genres.OrderBy(a => a.Name).ToListAsync();
            return genres;
        }
        public async Task<Genre> GetById(byte id)
        {
            var genre = await _db.Genres.SingleOrDefaultAsync(g => g.Id == id);
            return genre;
        }

        public async Task<Genre> UpdateGenre(Genre obj)
        {
            _db.Genres.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<Genre> DeleteGenre(Genre obj)
        {                
            _db.Remove(obj);
            await _db.SaveChangesAsync();
            return obj;
        }
        public async Task<bool> IsAllowedGenre(byte id)
        {
            var result = await _db.Genres.AnyAsync(g => g.Id == id);
            return result;
        }
    }
}
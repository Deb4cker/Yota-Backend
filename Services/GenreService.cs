using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;

namespace Yota_backend.Services;

public class GenreService : IGenreService
{
    private readonly IAppDbContext _context;

    public GenreService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Genre> GetGenreById(Guid id, CancellationToken token)
    {
        var genre = await _context.Genres
            .AsNoTracking()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        return genre;
    }

    public async Task<IEnumerable<Genre>> GetGenres(CancellationToken token)
    {
        var genres = await _context.Genres
            .AsNoTracking()
            .ToListAsync(token);

        return genres;
    }

    public async Task AddGenre(GenreDto genre, CancellationToken token)
    {
        var newGenre = new Genre
        {
            Name = genre.Name
        };

        await _context.Genres.AddAsync(newGenre, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateGenre(Guid id, GenreDto genre, CancellationToken token)
    {
        var genreToUpdate = await _context.Genres
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        if (genreToUpdate == null)
        {
            throw new KeyNotFoundException();
        }

        genreToUpdate.Name = genre.Name;

        _context.Genres.Update(genreToUpdate);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteGenre(Guid id, CancellationToken token)
    {
        var genreToDelete = await _context.Genres
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        if (genreToDelete == null)
        {
            throw new KeyNotFoundException();
        }

        _context.Genres.Remove(genreToDelete);
        await _context.SaveChangesAsync(token);
    }
}

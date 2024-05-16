using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;

namespace Yota_backend.Services;

public class GenreService : IGenreService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GenreService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Genre> GetGenreById(Guid id, CancellationToken token)
    {
        var genre = await _context.Genres
            .AsNoTracking()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token) ?? throw new KeyNotFoundException();
            
        return genre;
    }

    public IEnumerable<GenreDto> GetGenres()
    {
        var genres = _context.Genres
            .AsNoTracking();

        return _mapper.ProjectTo<GenreDto>(genres) ;
    }

    public async Task AddGenre(GenreRequest genre, CancellationToken token)
    {
        var newGenre = new Genre
        {
            Name = genre.Name
        };

        await _context.Genres.AddAsync(newGenre, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateGenre(Guid id, GenreRequest genre, CancellationToken token)
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
            .FirstOrDefaultAsync(token) ?? throw new KeyNotFoundException();
            
        _context.Genres.Remove(genreToDelete);
        await _context.SaveChangesAsync(token);
    }
}

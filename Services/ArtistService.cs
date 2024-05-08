using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;

namespace Yota_backend.Services;

public class ArtistService : IArtistService
{
    private readonly IAppDbContext _context;

    public ArtistService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Artist> GetArtistById(Guid id, CancellationToken token)
    {
        var artist = await _context.Artists
            .AsNoTracking()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        return artist;
    }

    public async Task<IEnumerable<Artist>> GetArtists(CancellationToken token)
    {
        var artists = await _context.Artists
            .AsNoTracking()
            .ToListAsync(token);

        return artists;
    }

    public async Task AddArtist(ArtistDto artist, CancellationToken token)
    {
        var newArtist = new Artist {Name = artist.Name};  

        _context.Artists.Add(newArtist);
        await _context.SaveChangesAsync(token);

        return;
    }

    public async Task UpdateArtist(Guid id, ArtistDto artist, CancellationToken token)
    {
        var record = await _context.Artists
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        record.Name = artist.Name;

        await _context.SaveChangesAsync(token);

        return;
    }

    public async Task DeleteArtist(Artist artist, CancellationToken token)
    {
        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync(token);

        return;
    }
}

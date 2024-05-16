using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;

namespace Yota_backend.Services;

public class ArtistService : IArtistService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;
    public ArtistService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Artist> GetArtistById(Guid id, CancellationToken token)
    {
        var artist = await _context.Artists
            .AsNoTracking()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        return artist;
    }

    public IEnumerable<ArtistDto> GetArtists()
    {
        var artists = _context.Artists
            .AsNoTracking();

        return _mapper.ProjectTo<ArtistDto>(artists);
    }

    public async Task AddArtist(ArtistRequest artist, CancellationToken token)
    {
        var newArtist = new Artist {Name = artist.Name};  

        _context.Artists.Add(newArtist);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateArtist(Guid id, ArtistRequest artist, CancellationToken token)
    {
        var record = await _context.Artists
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        record.Name = artist.Name;

        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteArtist(Artist artist, CancellationToken token)
    {
        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync(token);
    }
}

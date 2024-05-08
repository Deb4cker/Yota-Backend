using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface IArtistService
{
    Task<Artist> GetArtistById(Guid id, CancellationToken token);
    Task<IEnumerable<Artist>> GetArtists(CancellationToken token);
    Task AddArtist(ArtistDto artist, CancellationToken token);
    Task UpdateArtist(Guid id, ArtistDto artist, CancellationToken token);
}

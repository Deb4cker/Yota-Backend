using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface IArtistService
{
    Task<Artist> GetArtistById(Guid id, CancellationToken token);
    IEnumerable<ArtistDto> GetArtists();
    Task AddArtist(ArtistRequest artist, CancellationToken token);
    Task UpdateArtist(Guid id, ArtistRequest artist, CancellationToken token);
}

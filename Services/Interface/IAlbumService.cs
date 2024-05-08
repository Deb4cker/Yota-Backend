using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface IAlbumService
{
    Task AddAlbum(AlbumDto album, CancellationToken token);
    Task<Album> GetAlbumById(Guid id, CancellationToken token);
    Task<IEnumerable<Album>> GetAlbumsByArtistId(Guid ArtistId, CancellationToken token);
    Task UpdateAlbum(Guid id, AlbumDto album, CancellationToken token);
    Task DeleteAlbum(Guid id, CancellationToken token);
}

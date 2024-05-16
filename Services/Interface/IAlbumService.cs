using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface IAlbumService
{
    Task AddAlbum(AlbumRequest request, CancellationToken token);
    Task<AlbumDto> GetAlbumById(Guid id, CancellationToken token);
    IEnumerable<AlbumDto> GetAlbumsByArtistId(Guid artistId);
    Task UpdateAlbum(Guid id, AlbumRequest album, CancellationToken token);
    Task DeleteAlbum(Guid id, CancellationToken token);
    Task<AlbumCoverDto?> GetAlbumCover(Guid id, CancellationToken token);
}

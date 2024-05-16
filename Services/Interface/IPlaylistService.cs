using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface IPlaylistService
{
    Task CreatePlaylist(PlaylistRequest playlistRequest, CancellationToken token);
    Task DeletePlaylist(Guid id, CancellationToken token);
    Task<PlaylistDto> GetPlaylistById(Guid id, CancellationToken token);
    IEnumerable<PlaylistDto> GetPlaylistsByUserId(Guid userId);
    Task UpdatePlaylist(Guid id, PlaylistRequest playlistRequest, CancellationToken token);
    Task AddTrackToPlaylist(Guid playlistId, Guid trackId, CancellationToken token);
    Task RemoveTrackFromPlaylist(Guid playlistId, Guid trackId, CancellationToken token);
    Task <PlaylistCoverDto> GetPlaylistCover (Guid playlistId, CancellationToken token);
}

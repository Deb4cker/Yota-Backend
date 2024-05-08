using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface IPlaylistService
{
    Task CreatePlaylist(PlaylistDto playlistDto, CancellationToken token);
    Task DeletePlaylist(Guid id, CancellationToken token);
    Task<Playlist> GetPlaylistById(Guid id, CancellationToken token);
    Task<IEnumerable<Playlist>> GetPlaylists(CancellationToken token);
    Task UpdatePlaylist(Guid id, PlaylistDto playlistDto, CancellationToken token);
    Task AddTrackToPlaylist(Guid playlistId, Guid trackId, CancellationToken token);
    Task RemoveTrackFromPlaylist(Guid playlistId, Guid trackId, CancellationToken token);
}

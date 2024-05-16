using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface ITrackService
{
    Task<TrackDto> GetTrackById(Guid id, CancellationToken token);
    IEnumerable<TrackDto> GetTracksByAlbumId(Guid albumId);
    IEnumerable<TrackDto> GetTracksByGenreId(Guid genreId);
    IEnumerable<TrackDto> GetTracksByArtistId(Guid artistId);
    IEnumerable<TrackDto> GetTracksByPlaylistId(Guid playlistId);
    Task DeleteTrack(Guid trackId, CancellationToken token);
    Task AddTrack(TrackRequest track, CancellationToken token);
    Task UpdateTrack(Guid id, TrackRequest track, CancellationToken token);
    IEnumerable<byte[]> GetTrackFilesByPlaylistId(Guid playlistId);
    Task<byte[]> GetTrackFileById(Guid trackId, CancellationToken token);
}

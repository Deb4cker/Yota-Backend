using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface ITrackService
{
    Task<Track> GetTrackById(Guid id, CancellationToken token);
    Task<IEnumerable<Track>> GetTracksByAlbumId(Guid AlbumId, CancellationToken token);
    Task<IEnumerable<Track>> GetTracksByGenreId(Guid GenreId, CancellationToken token);
    Task<IEnumerable<Track>> GetTracksByArtistId(Guid ArtistId, CancellationToken token);
    Task DeleteTrack(Track track, CancellationToken token);
    Task AddTrack(Track track, CancellationToken token);
    Task UpdateTrack(Track track, CancellationToken token);
    Task<Track> MountNewTrack(TrackDto track, CancellationToken token);
}

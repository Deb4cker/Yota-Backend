using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;
using Yota_backend.Utils;

namespace Yota_backend.Services;

public class TrackService : ITrackService
{
    private readonly IAppDbContext _context;

    public TrackService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task AddTrack(Track track, CancellationToken token)
    {
        _context.Tracks.Add(track);
        await _context.SaveChangesAsync(token);

        return;
    }

    public async Task DeleteTrack(Track track, CancellationToken token)
    {
        _context.Tracks.Remove(track);
        await _context.SaveChangesAsync(token);
        
        return;
    }

    public async Task<Track> GetTrackById(Guid id, CancellationToken token)
    {
        var track = await _context.Tracks
            .AsNoTracking()
            .Where(t => t.Id == id)
            .FirstOrDefaultAsync(token);

        return track;
    }

    public async Task<IEnumerable<Track>> GetTracksByAlbumId(Guid albumId, CancellationToken token)
    {
        var tracks = await _context.Tracks
            .AsNoTracking()
            .Where(t => t.AlbumId == albumId)
            .ToListAsync(token);

        return tracks;
    }

    public Task<IEnumerable<Track>> GetTracksByArtistId(Guid ArtistId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Track>> GetTracksByArtistlistId(Guid artistId, CancellationToken token)
    {
        var tracks = await _context.Tracks
            .AsNoTracking()
            .Where(t => t.ArtistId == artistId)
            .ToListAsync(token);

        return tracks;
    }

    public async Task<IEnumerable<Track>> GetTracksByGenreId(Guid genreId, CancellationToken token)
    {
        var tracks = await _context.Tracks
            .AsNoTracking()
            .Where(t => t.GenreId == genreId)
            .ToListAsync(token);

        return tracks;
    }

    public async Task<Track> MountNewTrack(TrackDto track, CancellationToken token)
    {
        var isValidFormat = AudioExtension.IsAudioFormat(track.File.FileName);
        if (!isValidFormat)
        {
            throw new ArgumentException("Invalid audio format");
        }

        var milliseconds = AudioExtension.GetAudioDuration(track.File);
        var bytes = (int)track.File.Length;
        var byteString = await CryptTools.FromFileToStringByBytes(track.File);
        var AES = CryptTools.AESEncrypt(byteString);

        return new Track
        {
            Name = track.Name,
            ArtistId = track.ArtistId,
            AlbumId = track.AlbumId,
            GenreId = track.GenreId,
            Milliseconds = milliseconds,
            Bytes = bytes,
            AES = AES
        };
    }

    public async Task UpdateTrack(Track track, CancellationToken token)
    {
        var record = await GetTrackById(track.Id, token);

        if (record != null)
        {
            record = track;
        }
        await _context.SaveChangesAsync(token);
        return; 
    }
}
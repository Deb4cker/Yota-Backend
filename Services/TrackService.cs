using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Commons;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;
using Yota_backend.Utils;

namespace Yota_backend.Services;

public class TrackService (IAppDbContext context, IMapper mapper) : ITrackService
{
    public async Task AddTrack(TrackRequest track, CancellationToken token)
    {
        var newTrack = MountNewTrack(track);
        context.Tracks.Add(newTrack);
        await context.SaveChangesAsync(token);
    }

    public async Task<TrackDto> GetTrackById(Guid id, CancellationToken token)
    {
        var track = await context.Tracks.FindAsync([id, token], token);
        return mapper.Map<TrackDto>(track);
    }
    
    public IEnumerable<TrackDto> GetTracksByPlaylistId(Guid playlistId)
    {
        var tracks = context.Tracks
            .AsNoTracking()
            .Where(t => t.AlbumId == playlistId);

        return mapper.ProjectTo<TrackDto>(tracks);
    }

    public IEnumerable<TrackDto> GetTracksByAlbumId(Guid albumId)
    {
        var tracks = context.Tracks
            .AsNoTracking()
            .Where(t => t.AlbumId == albumId);

        return mapper.ProjectTo<TrackDto>(tracks);
    }

    public IEnumerable<TrackDto> GetTracksByGenreId(Guid genreId)
    {
        var tracks = context.Tracks
            .AsNoTracking()
            .Where(t => t.GenreId == genreId);

        return mapper.ProjectTo<TrackDto>(tracks);    }

    public IEnumerable<TrackDto> GetTracksByArtistId(Guid artistId)
    {
        var tracks = context.Tracks
            .AsNoTracking()
            .Where(t => t.ArtistId == artistId);

        return mapper.ProjectTo<TrackDto>(tracks);    
    }

    public async Task DeleteTrack(Guid id, CancellationToken token)
    {
        var track = await context.Tracks.FindAsync([id, token], token);
        if(track is not null) context.Tracks.Remove(track);
        await context.SaveChangesAsync(token);
    }

    private Track MountNewTrack(TrackRequest track)
    {
        var isValidFormat = AudioExtension.IsAudioFormat(track.File.FileName);
        if (!isValidFormat)
        {
            throw new ArgumentException("Invalid audio format");
        }

        var milliseconds = AudioExtension.GetAudioDuration(track.File);
        var bytes = (int)track.File.Length;
        var path = Constants.MusicFilePath + track.File.FileName;
        
        return new Track
        {
            Name = track.Name,
            Composer = track.Composer,
            ArtistId = track.ArtistId,
            AlbumId = track.AlbumId,
            GenreId = track.GenreId,
            Milliseconds = milliseconds,
            Bytes = bytes,
            FilePath = path
        };
    }

    public async Task UpdateTrack(Guid id, TrackRequest request, CancellationToken token)
    {
        var record = await context.Tracks.FindAsync([id, token]);

        if (record is not null)
        {
            record.Name = request.Name;
            record.AlbumId = request.AlbumId;
            record.ArtistId = request.ArtistId;
            record.GenreId = request.GenreId;
            record.Composer = request.Composer;
        }
        
        await context.SaveChangesAsync(token);
    }

    public IEnumerable<byte[]> GetTrackFilesByPlaylistId(Guid playlistId)
    {
        var playlist = context.Playlists.Find(playlistId);
        var tracks = playlist.Tracks;
        List<byte[]> files = [];
        files.AddRange(tracks.Select(track => File.ReadAllBytes(track.FilePath)));

        return files;
    }

    public async Task<byte[]> GetTrackFileById(Guid trackId, CancellationToken token)
    {
        var track = await context.Tracks.FindAsync([trackId, token], token);
        
        return await File.ReadAllBytesAsync(track.FilePath);
    }

    public async Task<byte[]> GetTrackFileByName(string name, CancellationToken token)
    {
        var track = await context.Tracks.FirstOrDefaultAsync(t=> t.Name == name, token);
        return await File.ReadAllBytesAsync(track.FilePath);
    }
}
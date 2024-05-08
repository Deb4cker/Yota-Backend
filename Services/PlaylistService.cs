using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;
using Yota_backend.Utils;

namespace Yota_backend.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IAppDbContext _context;

    public PlaylistService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Playlist> GetPlaylistById(Guid id, CancellationToken token)
    {
        var playlist = await _context.Playlists
            .AsNoTracking()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync(token);

        return playlist;
    }

    public async Task<IEnumerable<Playlist>> GetPlaylists(CancellationToken token)
    {
        var playlists = await _context.Playlists
            .AsNoTracking()
            .ToListAsync(token);

        return playlists;
    }

    public async Task CreatePlaylist(PlaylistDto playlistDto, CancellationToken token)
    {
        var playlist = new Playlist
        {
            Id = Guid.NewGuid(),
            Name = playlistDto.Name,
            Description = playlistDto.Description,
            ImageBlob = playlistDto.ImageBlob is not null? await CryptTools.FromFileToStringByBytes(playlistDto.ImageBlob) : null
        };

        await _context.Playlists.AddAsync(playlist);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdatePlaylist(Guid id, PlaylistDto playlistDto, CancellationToken token)
    {
        var playlist = await _context.Playlists
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync(token);

        playlist.Name = playlistDto.Name;
        playlist.Description = playlistDto.Description;
        playlist.ImageBlob = playlistDto.ImageBlob is not null ? await CryptTools.FromFileToStringByBytes(playlistDto.ImageBlob) : null;

        await _context.SaveChangesAsync(token);
    }

    public async Task DeletePlaylist(Guid id, CancellationToken token)
    {
        var playlist = await _context.Playlists
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync(token);

        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync(token);
    }

    public async Task AddTrackToPlaylist(Guid playlistId, Guid trackId, CancellationToken token)
    {
        var playlist = await _context.Playlists
            .Where(p => p.Id == playlistId)
            .Include(p => p.Tracks)
            .FirstOrDefaultAsync(token);

        var track = await _context.Tracks
            .Where(t => t.Id == trackId)
            .FirstOrDefaultAsync(token);

        playlist.Tracks.Add(track);
        await _context.SaveChangesAsync(token);
    }

    public async Task RemoveTrackFromPlaylist(Guid playlistId, Guid trackId, CancellationToken token)
    {
        var playlist = await _context.Playlists
            .Where(p => p.Id == playlistId)
            .Include(p => p.Tracks)
            .FirstOrDefaultAsync(token);

        var track = await _context.Tracks
            .Where(t => t.Id == trackId)
            .FirstOrDefaultAsync(token);

        playlist.Tracks.Remove(track);
        await _context.SaveChangesAsync(token);
    }
}

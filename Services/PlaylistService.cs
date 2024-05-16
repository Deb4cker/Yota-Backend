using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Commons;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;
using Yota_backend.Utils;

namespace Yota_backend.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public PlaylistService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlaylistDto> GetPlaylistById(Guid id, CancellationToken token)
    {
        var playlist = await _context.Playlists
            .AsNoTracking()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync(token) ?? throw new KeyNotFoundException();
        
        return _mapper.Map<PlaylistDto>(playlist);
    }

    public IEnumerable<PlaylistDto> GetPlaylistsByUserId(Guid userId)
    {
        var playlists = _context.Playlists
            .AsNoTracking()
            .Where(p => p.UserId == userId);
        
        return _mapper.ProjectTo<PlaylistDto>(playlists)
            .ToList();
    }

    public async Task CreatePlaylist(PlaylistRequest playlistRequest, CancellationToken token)
    {
        var file = playlistRequest.ImageFile;
        if (file is not null)
        {
            await using var stream = new FileStream(Constants.PlaylistCoverPath, FileMode.Create);
            await file.CopyToAsync(stream, token);
        }
        
        var playlist = new Playlist
        {
            Id = Guid.NewGuid(),
            Title = playlistRequest.Name,
            Description = playlistRequest.Description,
            ImagePath = file is not null
                ? Constants.PlaylistCoverPath + file.FileName
                : null
        };

        await _context.Playlists.AddAsync(playlist, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdatePlaylist(Guid id, PlaylistRequest playlistRequest, CancellationToken token)
    {
        var playlist = await _context.Playlists
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync(token);

        if (playlist is not null)
        {
            playlist.Title = playlistRequest.Name;
            playlist.Description = playlistRequest.Description;
            playlist.ImagePath = playlistRequest.ImageFile is not null
                ? Constants.PlaylistCoverPath + playlistRequest.ImageFile.FileName
                : null;
        }

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

    public async Task<PlaylistCoverDto> GetPlaylistCover(Guid playlistId, CancellationToken token)
    {
        var playlist = await _context.Playlists.FindAsync([playlistId, token], cancellationToken: token);
        var path = playlist?.ImagePath;
        var image = string.IsNullOrEmpty(path)
            ? DefaultByteFiles.GetDefaultPlaylistCoverBytes()
            : await File.ReadAllBytesAsync(path, token);
        
        return new PlaylistCoverDto { Image = image };
    }
}

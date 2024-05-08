using Microsoft.AspNetCore.Mvc;
using Yota_backend.Controllers.Dto;
using Yota_backend.Services.Interface;

namespace Yota_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlaylistController : ControllerBase
{
    private readonly IPlaylistService _playlistService;

    public PlaylistController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlaylists(CancellationToken token)
    {
        var playlists = await _playlistService.GetPlaylists(token);

        return Ok(playlists);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlaylistById(Guid id, CancellationToken token)
    {
        var playlist = await _playlistService.GetPlaylistById(id, token);

        return Ok(playlist);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlaylist([FromForm] PlaylistDto playlistDto, CancellationToken token)
    {
        await _playlistService.CreatePlaylist(playlistDto, token);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlaylist(Guid id, [FromForm] PlaylistDto playlistDto, CancellationToken token)
    {
        await _playlistService.UpdatePlaylist(id, playlistDto, token);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlaylist(Guid id, CancellationToken token)
    {
        await _playlistService.DeletePlaylist(id, token);

        return Ok();
    }

    [HttpPost("{playlistId}/track/{trackId}")]
    public async Task<IActionResult> AddTrackToPlaylist(Guid playlistId, Guid trackId, CancellationToken token)
    {
        await _playlistService.AddTrackToPlaylist(playlistId, trackId, token);

        return Ok();
    }

    [HttpDelete("{playlistId}/track/{trackId}")]
    public async Task<IActionResult> RemoveTrackFromPlaylist(Guid playlistId, Guid trackId, CancellationToken token)
    {
        await _playlistService.RemoveTrackFromPlaylist(playlistId, trackId, token);

        return Ok();
    }
}

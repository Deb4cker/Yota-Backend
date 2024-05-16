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

    [HttpGet("userPlaylists/{userId}")]
    public IActionResult GetPlaylists(Guid userId)
    {
        return Ok(_playlistService.GetPlaylistsByUserId(userId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlaylistById(Guid id, CancellationToken token)
    {
        try
        {
            var playlist = await _playlistService.GetPlaylistById(id, token);
            return Ok(playlist);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlaylist([FromForm] PlaylistRequest playlistRequest, CancellationToken token)
    {
        await _playlistService.CreatePlaylist(playlistRequest, token);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlaylist(Guid id, [FromForm] PlaylistRequest playlistRequest, CancellationToken token)
    {
        await _playlistService.UpdatePlaylist(id, playlistRequest, token);
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

    [HttpGet("cover/{playlistId}")]
    public async Task<ActionResult<byte[]>> GetPlaylistCover(Guid id, CancellationToken token)
    {
        var cover = await _playlistService.GetPlaylistCover(id, token);
        return File(cover.Image, "image/png", "png");
    }
    
    
}

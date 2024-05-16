using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;

namespace Yota_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController : ControllerBase
{
    private readonly IAlbumService _service;

    public AlbumController(IAlbumService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Album>> GetAlbum(Guid id, CancellationToken token)
    {
        var album = await _service.GetAlbumById(id, token);
        return Ok(album);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Album>> GetAlbumsByArtistId(Guid id)
    {
        var albums = _service.GetAlbumsByArtistId(id);
        return Ok(albums);
    }

    [HttpPost]
    public async Task<ActionResult<Album>> PostAlbum(AlbumRequest album, CancellationToken token)
    {
        await _service.AddAlbum(album, token);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAlbum(Guid id, AlbumRequest album, CancellationToken token)
    {
        try
        {
            await _service.UpdateAlbum(id, album, token);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(Guid id, CancellationToken token)
    {
        try
        {
            await _service.DeleteAlbum(id, token);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        return Ok();
    }

    [HttpGet("cover/{id}")]
    public async Task<ActionResult<byte[]>> GetAlbumCover(Guid id, CancellationToken token)
    {
        var cover = await _service.GetAlbumCover(id, token);
        
        if (cover is not null) return File(cover.Image, "image/png", "png");
        return NotFound();
    }
}

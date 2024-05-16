using Microsoft.AspNetCore.Mvc;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;

namespace Yota_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtistController : ControllerBase
{

    private readonly IArtistService _service;

    public ArtistController(IArtistService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Artist>> GetArtist(Guid id, CancellationToken token)
    {
        var artist = await _service.GetArtistById(id, token);

        if (artist == null)
        {
            return NotFound();
        }

        return Ok(artist);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Artist>> GetArtists()
    { 
        return Ok(_service.GetArtists());
    }

    [HttpPost]
    public async Task<ActionResult<Artist>> PostArtist(ArtistRequest artist, CancellationToken token)
    {
        await _service.AddArtist(artist, token);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutArtist(Guid id, ArtistRequest artist, CancellationToken token)
    {
        try
        {
            await _service.UpdateArtist(id, artist, token);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}

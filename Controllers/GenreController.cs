using Microsoft.AspNetCore.Mvc;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;

namespace Yota_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _service;

    public GenreController(IGenreService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Genre>> GetGenre(Guid id, CancellationToken token)
    {
        try
        {
            var genre = await _service.GetGenreById(id, token);
            return Ok(genre);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<Genre>> GetGenres(CancellationToken token)
    {
        return Ok(_service.GetGenres());
    }

    [HttpPost]
    public async Task<ActionResult<Genre>> PostGenre(GenreRequest genre, CancellationToken token)
    {
        await _service.AddGenre(genre, token);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(Guid id, GenreRequest genre, CancellationToken token)
    {
        try
        {
            await _service.UpdateGenre(id, genre, token);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(Guid id, CancellationToken token)
    {
        try
        {
            await _service.DeleteGenre(id, token);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        return Ok();
    }
}

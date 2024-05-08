using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;

namespace Yota_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;

        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Track>> GetTrack(Guid id, CancellationToken token)
        {
            var track = await _trackService.GetTrackById(id, token);

            if (track == null)
            {
                return NotFound();
            }

            return Ok(track);
        }

        [HttpPost]
        public async Task<ActionResult<Track>> PostTrack(TrackDto track, CancellationToken token)
        {
            var mountedTrack = await _trackService.MountNewTrack(track, token);
            await _trackService.AddTrack(mountedTrack, token);

            return CreatedAtAction("GetTrack", new { id = mountedTrack.Id }, track);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrack(Guid id, Track track, CancellationToken token)
        {
            if (id != track.Id)
            {
                return BadRequest();
            }

            try
            {
                await _trackService.UpdateTrack(track, token);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TrackExists(id, token))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(Guid id, CancellationToken token)
        {
            var track = await _trackService.GetTrackById(id, token);
            if (track == null)
            {
                return NotFound();
            }

            await _trackService.DeleteTrack(track, token);

            return NoContent();
        }

        private async Task<bool> TrackExists(Guid id, CancellationToken token)
        {
            return await _trackService.GetTrackById(id, token) != null;
        }   
    }
}

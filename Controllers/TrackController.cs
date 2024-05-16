using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var track = await _trackService.GetTrackById(id, token);
                return Ok(track);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTrack(TrackRequest track, CancellationToken token)
        {
            await _trackService.AddTrack(track, token);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutTrack(Guid id, TrackRequest track, CancellationToken token)
        {
            await _trackService.UpdateTrack(id, track, token);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrack(Guid id, CancellationToken token)
        {
            await _trackService.DeleteTrack(id, token);
            return NoContent();
        }
        
        [HttpGet("file/{id}")]
        public async Task <ActionResult<byte[]>> GetMusicFileById(Guid id, CancellationToken token)
        {
            var bytes = await _trackService.GetTrackFileById(id, token);
            return File(bytes, "application/octet-stream", "track.mp3");
        }

        [HttpGet("files/{id}")]
        public ActionResult<IEnumerable<byte[]>> GetMusicFilesByPlaylistId(Guid id, CancellationToken token)
        {
            var bytes = _trackService.GetTrackFilesByPlaylistId(id);

            List < FileContentResult > contentResults= [];
            var count = 0;

            contentResults.AddRange(bytes.Select(track =>
                new FileContentResult(track, "application/octet-stream") 
                    { FileDownloadName = $"track{++count}.mp3" }
            ));
            return Ok(contentResults);
        }   
    }
}

namespace Yota_backend.Controllers.Dto;

public class PlaylistDto
{
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public List<Guid>? TrackIds { get; set; }
    public string Description { get; set; }
    public IFormFile? ImageBlob { get; set; }
}

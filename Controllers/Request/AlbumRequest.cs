namespace Yota_backend.Controllers.Dto;

public record AlbumRequest
{
    public string Title { get; init; }
    public Guid ArtistId { get; init; }
    public IFormFile? Image { get; init; }
}

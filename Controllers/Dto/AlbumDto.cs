namespace Yota_backend.Controllers.Dto;

public record AlbumDto
{
    public string Title { get; init; }
    public Guid ArtistId { get; init; }
    public Guid GenreId { get; init; }
    public IFormFile Image { get; init; }
}

namespace Yota_backend.Controllers.Dto;

public record TrackDto
{
    public string Name { get; init; }
    public Guid ArtistId { get; init; }
    public Guid AlbumId { get; init; }
    public Guid GenreId { get; init; }
    public IFormFile File { get; init; }
}

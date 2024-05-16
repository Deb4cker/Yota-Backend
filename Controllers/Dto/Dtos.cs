using Microsoft.AspNetCore.Mvc;

namespace Yota_backend.Controllers.Dto;

public record PlaylistDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public List<TrackDto> Tracks { get; init; } = [];
}

public record PlaylistCoverDto
{
    public byte[] Image { get; init; }
}

public class GenreDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}

public record ArtistDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public List<AlbumDto>? Albums { get; set; } = [];
}

public record AlbumDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public ArtistDto Artist { get; init; }
    public IList<TrackDto> Tracks { get; set; } = [];
}

public record AlbumCoverDto
{
    public byte[] Image { get; init; }
}

public record TrackDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public ArtistDto Artist { get; init; }
    public ArtistDto Album { get; init; }
    public GenreDto Genre { get; init; }
}

public record TrackFileDto
{
    public byte[] TrackFile { get; init; }
}

public record PlaylistTrackFilesDto
{
    public List<FileContentResult> files = [];
}

public record AlbumTrackFilesDto
{
    public List<FileContentResult> files = [];
}

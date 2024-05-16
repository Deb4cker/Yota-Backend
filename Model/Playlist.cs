namespace Yota_backend.Model;

public class Playlist
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public string? ImagePath { get; set; }

    public virtual IList<Track> Tracks { get; set; } = [];
}
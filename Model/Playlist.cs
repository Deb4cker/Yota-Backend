namespace Yota_backend.Model;

public class Playlist
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageBlob { get; set; }

    public virtual IList<Track> Tracks { get; set; } = [];
}
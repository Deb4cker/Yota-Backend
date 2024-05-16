namespace Yota_backend.Model;

public class Album
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ImagePath { get; set; }
    public Guid ArtistId { get; set; }
    public virtual Artist Artist { get; set; }
    public virtual IList<Track> Tracks { get; set; } = [];
}
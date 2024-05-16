namespace Yota_backend.Model;

public class Track
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Composer { get; set; }
    public long Milliseconds { get; set; }
    public long Bytes { get; set; }
    public string FilePath { get; set; }

    public Guid ArtistId { get; set; }
    public virtual Artist Artist { get; set; }
    public Guid AlbumId { get; set; }
    public virtual Album Album { get; set; }
    public Guid GenreId { get; set; }
    public virtual Genre Genre { get; set; }

    public virtual IList<Playlist> Playlists { get; set; } = [];
}
namespace Yota_backend.Model;

public class Artist
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual IList<Album> Albums { get; set; } = [];
}
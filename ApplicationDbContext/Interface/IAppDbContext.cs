using Microsoft.EntityFrameworkCore;
using Yota_backend.Model;

namespace Yota_backend.ApplicationDbContext.Interface;

public interface IAppDbContext
{
    DbSet<Track> Tracks { get; }
    DbSet<Album> Albums { get; }
    DbSet<Artist> Artists { get; }
    DbSet<Genre> Genres { get; }
    DbSet<Playlist> Playlists { get; }
    Task SaveChangesAsync(CancellationToken token);
}

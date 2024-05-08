using Microsoft.EntityFrameworkCore;
using System.Text;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;
using Yota_backend.Utils;

namespace Yota_backend.Services;

public class AlbumService : IAlbumService
{
    private readonly IAppDbContext _context;

    public AlbumService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Album> GetAlbumById(Guid id, CancellationToken token)
    {
        var album = await _context.Albums
            .AsNoTracking()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        return album;
    }

    public async Task<IEnumerable<Album>> GetAlbumsByArtistId(Guid ArtistId, CancellationToken token)
    {
        var albums = await _context.Albums
            .AsNoTracking()
            .Where(a => a.ArtistId == ArtistId)
            .ToListAsync(token);

        return albums;
    }

    public async Task AddAlbum(AlbumDto album, CancellationToken token)
    {
        var blob = await CryptTools.FromFileToStringByBytes(album.Image);

        var newAlbum = new Album
        {
            Id = Guid.NewGuid(),
            Title = album.Title,
            ArtistId = album.ArtistId,
            ImageBlob = CryptTools.AESEncrypt(blob)
        };

        await _context.Albums.AddAsync(newAlbum, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAlbum(Guid id, AlbumDto album, CancellationToken token)
    {
        var albumToUpdate = await _context.Albums
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        if (albumToUpdate == null)
        {
            throw new KeyNotFoundException();
        }

        albumToUpdate.Title = album.Title;
        albumToUpdate.ArtistId = album.ArtistId;

        _context.Albums.Update(albumToUpdate);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAlbum(Guid id, CancellationToken token)
    {
        var albumToDelete = await _context.Albums
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        if (albumToDelete == null)
        {
            throw new KeyNotFoundException();
        }

        _context.Albums.Remove(albumToDelete);
        await _context.SaveChangesAsync(token);
    }
}

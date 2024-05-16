using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Commons;
using Yota_backend.Controllers.Dto;
using Yota_backend.Model;
using Yota_backend.Services.Interface;
using Yota_backend.Utils;

namespace Yota_backend.Services;

public class AlbumService : IAlbumService
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public AlbumService(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AlbumDto> GetAlbumById(Guid id, CancellationToken token)
    {
        var album = await _context.Albums
            .AsNoTracking()
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync(token);

        return _mapper.Map<AlbumDto>(album);
    }

    public IEnumerable<AlbumDto> GetAlbumsByArtistId(Guid artistId)
    {
        var albums = _context.Albums
            .AsNoTracking()
            .Where(a => a.ArtistId == artistId);

        return _mapper.ProjectTo<AlbumDto>(albums);
    }

    public async Task AddAlbum(AlbumRequest request, CancellationToken token)
    {
        var coverFile = request.Image;

        var imagePath = Constants.DefaultAlbumCoverImage;
        if (coverFile is not null)
        {
            imagePath = Constants.AlbumCoverPath + coverFile.FileName;
        }

        var album = new Album
        {
            ArtistId = request.ArtistId,
            Title = request.Title,
            ImagePath = imagePath
        };

        _context.Albums.Add(album);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAlbum(Guid id, AlbumRequest album, CancellationToken token)
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

        if (albumToDelete == null) throw new KeyNotFoundException();

        _context.Albums.Remove(albumToDelete);
        await _context.SaveChangesAsync(token);
    }

    public async Task<AlbumCoverDto?> GetAlbumCover(Guid id, CancellationToken token)
    {
        var album = await _context.Albums.FindAsync([id, token], cancellationToken: token);
        var path = album?.ImagePath;
        var image = string.IsNullOrEmpty(path)
            ? DefaultByteFiles.GetDefaultAlbumCoverBytes()
            : await File.ReadAllBytesAsync(path, token);
        
        return new AlbumCoverDto { Image = image };
    }
}

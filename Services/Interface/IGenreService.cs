using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface IGenreService
{
    Task AddGenre(GenreDto genre, CancellationToken token);
    Task DeleteGenre(Guid id, CancellationToken token);
    Task<Genre> GetGenreById(Guid id, CancellationToken token);
    Task<IEnumerable<Genre>> GetGenres(CancellationToken token);
    Task UpdateGenre(Guid id, GenreDto genre, CancellationToken token);
}

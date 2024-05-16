using Yota_backend.Controllers.Dto;
using Yota_backend.Model;

namespace Yota_backend.Services.Interface;

public interface IGenreService
{
    Task AddGenre(GenreRequest genre, CancellationToken token);
    Task DeleteGenre(Guid id, CancellationToken token);
    Task<Genre> GetGenreById(Guid id, CancellationToken token);
    IEnumerable<GenreDto> GetGenres();
    Task UpdateGenre(Guid id, GenreRequest genre, CancellationToken token);
}

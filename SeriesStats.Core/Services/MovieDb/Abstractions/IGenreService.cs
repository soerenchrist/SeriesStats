using SeriesStats.Core.Models.MovieDb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriesStats.Core.Services.MovieDb.Abstractions
{
    public interface IGenreService
    {
        Task<Genre> GetGenreById(int genreId);
        Task<IList<Genre>> GetAll();
    }
}
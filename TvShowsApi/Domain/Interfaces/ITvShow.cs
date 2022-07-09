using Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces

{
    public interface ITvShow
    {
        Task<TvShow> SearchById(int Id);
        Task<IEnumerable<TvShow>> SearchAll();
        Task<IEnumerable<TvShow>> SearchByName(string name);
        Task<TvShow> AddTvShowToFavorites(string name);
        Task<TvShow> RemoveTvShowToFavorites(string name);
        Task<IEnumerable<Episode>> GetEpisodesByTvShow(string name);

    }
}

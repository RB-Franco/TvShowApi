using Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IApplicationTvShow
    {
        Task<TvShow> SearchById(int id);
        Task<IEnumerable<TvShow>> SearchAll();
        Task<IEnumerable<TvShow>> SearchByName(string name);
        Task<TvShow> AddTvShowToFavorites(TvShow tvShow);
        Task<TvShow> RemoveTvShowToFavorites(Favorites favorite);
        Task<IEnumerable<Episode>> GetEpisodesByTvShowId(int tvShowId);
    }
}

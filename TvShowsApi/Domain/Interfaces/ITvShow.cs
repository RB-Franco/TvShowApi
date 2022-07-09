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
        Task<TvShow> AddTvShowToFavorites(TvShow tvshow);
        Task<TvShow> RemoveTvShowToFavorites(Favorites favorite);
        Task<IEnumerable<Episode>> GetEpisodesByTvShowId(int tvShowId);

    }
}

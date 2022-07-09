using Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces

{
    public interface ITvShow
    {
        Task<IEnumerable<TvShow>> GetAllTvShows();
        Task<IEnumerable<TvShow>> SearchByName(string name);
        Task<IEnumerable<Favorite>> GetAllFavoritesByUserId(string userId);
        Task<Favorite> AddTvShowToFavorites(TvShow tvshow, string userId);
        Task<bool> RemoveTvShowToFavorites(Favorite favorite);
        Task<IEnumerable<Episode>> GetEpisodesByTvShowId(int tvShowId);

    }
}

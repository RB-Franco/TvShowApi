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
        Task<IEnumerable<Favorite>> GetAllFavoritesByUserId(string userId);
        Task<TvShow> AddTvShowToFavorites(TvShow tvshow, string userId);
        Task<bool> RemoveTvShowToFavorites(Favorite favorite);
        Task<IEnumerable<Episode>> GetEpisodesByTvShowId(int tvShowId);

    }
}

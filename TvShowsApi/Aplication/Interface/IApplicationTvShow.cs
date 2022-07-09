using Entity.Entity;
using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IApplicationTvShow
    {
        Task<TvShowModel> SearchById(int id);
        Task<IEnumerable<TvShowModel>> SearchAll();
        Task<IEnumerable<TvShowModel>> SearchByName(string name);
        Task<IEnumerable<EpisodeModel>> GetEpisodesByTvShowId(int tvShowId);
        Task<IEnumerable<FavoriteModel>> GetAllFavoritesByUserId(string userId);
        Task<TvShowModel> AddTvShowToFavorites(TvShowModel tvShow, string userId);
        Task<bool> RemoveTvShowToFavorites(FavoriteModel favorite);
    }
}

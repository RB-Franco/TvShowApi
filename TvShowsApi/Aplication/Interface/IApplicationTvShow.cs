using Entity.Entity;
using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IApplicationTvShow
    {
        Task<IEnumerable<TvShowModel>> GetAllTvShows();
        Task<IEnumerable<TvShowModel>> GetAllTvShowsByName(string name);
        Task<IEnumerable<TvShowModel>> GetAllTvShowsByGenere(string genere);
        Task<TvShowModel> GetTvShowById(int tvShowId);
        Task<IEnumerable<EpisodeModel>> GetEpisodesByTvShowId(int tvShowId);
        Task<IEnumerable<FavoriteModel>> GetAllFavoritesByUserId(string userId);
        Task<FavoriteModel> AddTvShowToFavorites(TvShowModel tvShow, string userId);
        Task<bool> RemoveTvShowToFavorites(FavoriteModel favorite);
    }
}

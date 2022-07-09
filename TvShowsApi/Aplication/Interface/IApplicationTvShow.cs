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
        Task<TvShow> AddTvShowToFavorites(string name);
        Task<TvShow> RemoveTvShowToFavorites(string name);
        Task<IEnumerable<Episode>> GetEpisodesByTvShow(string name);
    }
}

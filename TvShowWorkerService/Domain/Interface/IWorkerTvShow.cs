using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowWorkerService.Models;

namespace TvShowWorkerService.Domain.Interface
{
    public interface IWorkerTvShow
    {
        Task<bool> AddTvShow(TvShowModel model);
        Task<bool> AddTvShowEpisodes(IEnumerable<EpisodesModel> model, int tvShowId);
    }
}

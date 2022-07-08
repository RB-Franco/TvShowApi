using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerService.Models;

namespace WorkerService.Domain.Inteface
{
    public interface IWorkerTvShow
    {
        Task<bool> AddTvShow(TvShowModel model);
        Task<bool> AddTvShowEpisodes(IEnumerable<EpisodesModel> model, int referenceId);
    }
}

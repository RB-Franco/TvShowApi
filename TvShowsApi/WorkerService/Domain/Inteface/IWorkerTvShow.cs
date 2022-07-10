using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerService.Models;

namespace WorkerService.Domain.Inteface
{
    public interface IWorkerTvShow
    {
        Task<bool> AddTvShow(TvShowModel model, int lastPage, int totalPage);
        Task<Tuple<int, int>> GetLastPage();
        Task<bool> AddTvShowEpisodes(IEnumerable<EpisodesModel> model, int referenceId);
        Task<bool> RemoveTvShowEpisodes(TvShowModel model);

    }
}

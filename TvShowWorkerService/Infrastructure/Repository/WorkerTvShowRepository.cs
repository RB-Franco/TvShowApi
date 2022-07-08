using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TvShowWorkerService.Domain.Interface;
using TvShowWorkerService.Infrastructure.Configuration;
using TvShowWorkerService.Infrastructure.Entity;
using TvShowWorkerService.Models;

namespace TvShowWorkerService.Infrastructure.Repository
{
    public class WorkerTvShowRepository : IWorkerTvShow
    {
        private readonly DbContextOptions<WorkenContext> _optionsBuilder;

        public WorkerTvShowRepository()
        {
            _optionsBuilder = new DbContextOptions<WorkenContext>();
        }
        public async Task<bool> AddTvShow(TvShowModel TvShows)
        {
            try
            {             
                using (var context = new WorkenContext(_optionsBuilder))
                {
                    await context.WorkerTvShow.AddAsync(new WorkerTvShow
                    {
                        //preencher o restante do objeto
                        ReferenceId = TvShows.ReferenceId,
                        Name = TvShows.Name,
                        Permalink = TvShows.Permalink,
                        StartDate = TvShows.StartDate,
                        EndDate = TvShows.EndDate,
                        Country = TvShows.Country,
                        Network = TvShows.Network,
                        Status = TvShows.Status,
                        Image = TvShows.Image
                    });
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> AddTvShowEpisodes(IEnumerable<EpisodesModel> Episodes, int tvShowId)
        {
            try
            {
                var listEpisodes = new List<WorkerEpisode>();
                foreach (var episode in Episodes)
                {
                    listEpisodes.Add(new WorkerEpisode
                    {
                        Id = tvShowId,
                        Season = episode.Season,
                        Number = episode.number,
                        Name = episode.Name,
                        AirDate = episode.AirDate
                    });
                }

                using (var context = new WorkenContext(_optionsBuilder))
                {
                    await context.WorkerEpisode.AddRangeAsync(listEpisodes);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }

}

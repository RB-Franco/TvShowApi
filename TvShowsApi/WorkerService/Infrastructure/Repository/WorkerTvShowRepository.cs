using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Domain.Inteface;
using WorkerService.Infrastructure.Configuration;
using WorkerService.Infrastructure.Entity;
using WorkerService.Models;

namespace WorkerService.Infrastructure.Repository
{
    public class WorkerTvShowRepository : IWorkerTvShow
    {
        private readonly DbContextOptions<WorkenContext> _optionsBuilder;

        public WorkerTvShowRepository()
        {
            _optionsBuilder = new DbContextOptions<WorkenContext>();
        }
        public async Task<bool> AddTvShow(TvShowModel tvShow, int lastPage, int totalPage)
        {
            try
            {
                using (var context = new WorkenContext(_optionsBuilder))
                {
                    await context.WorkerTvShow.AddAsync(new WorkerTvShow
                    {
                        ReferenceId = tvShow.ReferenceId,
                        Name = tvShow.Name,
                        Permalink = tvShow.Permalink,
                        StartDate = tvShow.StartDate,
                        EndDate = tvShow.EndDate,
                        Country = tvShow.Country,
                        Network = tvShow.Network,
                        Status = tvShow.Status,
                        ImagePath = tvShow.ImagePath,
                        Url = tvShow.Url,
                        Description = tvShow.Description,
                        DescriptionSource = tvShow.DescriptionSource,
                        Runtime = tvShow.Runtime,
                        Genres = string.Join(",", tvShow.Genres)
                    });
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
            return false;
        }

        public async Task<bool> AddTvShowEpisodes(IEnumerable<EpisodesModel> Episodes, int referenceId)
        {
            try
            {
                var listEpisodes = new List<WorkerEpisode>();
                var IdTvShow = await GetIdTvShowByReferenceId(referenceId);
                foreach (var episode in Episodes)
                {
                    listEpisodes.Add(new WorkerEpisode
                    {
                        ShowId = IdTvShow,
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

        public async Task<bool> RemoveTvShowEpisodes(TvShowModel tvShow)
        {
            try
            {
                var IdTvShow = await GetIdTvShowByReferenceId(tvShow.ReferenceId);
                using (var context = new WorkenContext(_optionsBuilder))
                {
                    context.WorkerTvShow.Remove(new WorkerTvShow
                    {
                        Id = IdTvShow,
                        ReferenceId = tvShow.ReferenceId,
                        Name = tvShow.Name,
                        Permalink = tvShow.Permalink,
                        StartDate = tvShow.StartDate,
                        EndDate = tvShow.EndDate,
                        Country = tvShow.Country,
                        Network = tvShow.Network,
                        Status = tvShow.Status,
                        ImagePath = tvShow.ImagePath,
                        Url = tvShow.Url,
                        Description = tvShow.Description,
                        DescriptionSource = tvShow.DescriptionSource,
                        Runtime = tvShow.Runtime,
                        Genres = string.Join(",", tvShow.Genres)
                    });
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
                return false;
            }
            return true;
        }
        public async Task<Tuple<int, int>> GetLastPage()
        {
            try
            {
                using (var context = new WorkenContext(_optionsBuilder))
                {
                    var result = await context.WorkerTvShow.OrderByDescending(x => x.Page)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();
                    return new Tuple<int, int>(result.Page, result.TotalPage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
                return new Tuple<int, int>(0, 0);
            }
        }

        private async Task<int> GetIdTvShowByReferenceId(int referenceId)
        {
            try
            {
                using (var context = new WorkenContext(_optionsBuilder))
                {
                    var result = await context.WorkerTvShow
                                        .Where(x => x.ReferenceId == referenceId)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();
                    return result.Id;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
                return 0;
            }
        }
    }
}

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using WorkerService.Domain.Inteface;
using WorkerService.Models;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWorkerTvShow _IWorkerTvShow;
        HttpClient client = new HttpClient();


        public Worker(ILogger<Worker> logger, IWorkerTvShow IWorkerTvShow)
        {
            _logger = logger;
            _IWorkerTvShow = IWorkerTvShow;
    }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            _logger.LogInformation("Worker has been stoped", DateTimeOffset.Now);
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await GetTvShowsPerPage();
                if (result)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay((60 * 1000), stoppingToken);
            }
        }

        private async Task<bool> AddTvShows(TvShowModel tvShow, int lastPage, int totalPage)
        {
            var result = await _IWorkerTvShow.AddTvShow(tvShow, lastPage, totalPage);
            return result;
        }

        private async Task<Tuple<int, int>> GetLastPage()
        {
            var result = await _IWorkerTvShow.GetLastPage();
            return result;
        }

        private async Task<bool> AddTvShowsEpisodes(IEnumerable<EpisodesModel> tvShow, int referenceId)
        {
            var result = await _IWorkerTvShow.AddTvShowEpisodes(tvShow, referenceId);
            return result;
        }

        private async Task<bool> RemoveTvShowsEpisodes(TvShowModel tvShow)
        {
            var result = await _IWorkerTvShow.RemoveTvShowEpisodes(tvShow);
            return result;
        }

        private async Task<bool> GetTvShowsPerPage()
        {
            var lastPage = await GetLastPage();

            if (lastPage.Item2 >= 0 && (lastPage.Item1 == lastPage.Item2))
                return true;

            var page = lastPage.Item1 + 1;
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://www.episodate.com/api/most-popular?page={page}")
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<ResponseTvShowModel>(await response.Content.ReadAsStringAsync());
                        foreach (var item in result.TvShows)
                        {
                            var tvShowDetail = await GetTvShowsDetailsById(item.ReferenceId);
                            if (tvShowDetail != null)
                            {
                                if (await AddTvShows(tvShowDetail, result.Page, result.Pages))
                                {
                                    var added = await AddTvShowsEpisodes(tvShowDetail.Episodes, tvShowDetail.ReferenceId);
                                    if (!added)
                                        await RemoveTvShowsEpisodes(tvShowDetail);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
                return false;
            }

            return true;
        }

        private async Task<TvShowModel> GetTvShowsDetailsById(int idShow)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage { Method = HttpMethod.Get, RequestUri = new Uri($"https://www.episodate.com/api/show-details?q={idShow}") };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<tvShowDetails>(await response.Content.ReadAsStringAsync());
                        return result.TvShow;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }
    }
}
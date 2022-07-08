using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TvShowWorkerService.Domain.Interface;
using TvShowWorkerService.Infrastructure.Entity;
using TvShowWorkerService.Models;

namespace TvShowWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWorkerTvShow _IWorkerTvShow;
        private int SearchPage = 1;

        public Worker(ILogger<Worker> logger, IWorkerTvShow IWorkerTvShow)
        {
            _logger = logger;
            _IWorkerTvShow = IWorkerTvShow;
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
                await Task.Delay((60*1000), stoppingToken);
            }
        }

        private async Task<bool> AddTvShows(TvShowModel tvShow)
        {
            var result = await _IWorkerTvShow.AddTvShow(tvShow);
            return result;
        }

        private async Task<bool> AddTvShowsEpisodes(IEnumerable<EpisodesModel> tvShow, int tvShowId)
        {
            var result = await _IWorkerTvShow.AddTvShowEpisodes(tvShow, tvShowId);
            return result;
        }

        private async Task<bool> GetTvShowsPerPage()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://www.episodate.com/api/most-popular?page={SearchPage}")
            };

            //var json = "{\"total\":\"21682\",\"page\":1,\"pages\":1085,\"tv_shows\":[{\"id\":35624,\"name\":\"TheFlash\",\"permalink\":\"the-flash\",\"start_date\":\"2014-10-07\",\"end_date\":null,\"country\":\"US\",\"network\":\"TheCW\",\"status\":\"Running\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/35624.jpg\"},{\"id\":23455,\"name\":\"GameofThrones\",\"permalink\":\"game-of-thrones\",\"start_date\":\"2011-04-17\",\"end_date\":null,\"country\":\"US\",\"network\":\"HBO\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/23455.jpg\"},{\"id\":29560,\"name\":\"Arrow\",\"permalink\":\"arrow\",\"start_date\":\"2012-10-10\",\"end_date\":null,\"country\":\"US\",\"network\":\"TheCW\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/29560.jpg\"},{\"id\":43467,\"name\":\"Lucifer\",\"permalink\":\"lucifer\",\"start_date\":\"2016-01-25\",\"end_date\":null,\"country\":\"US\",\"network\":\"Netflix\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/43467.com\"},{\"id\":43234,\"name\":\"Supergirl\",\"permalink\":\"supergirl\",\"start_date\":\"2015-10-26\",\"end_date\":null,\"country\":\"US\",\"network\":\"TheCW\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/43234.jpg\"},{\"id\":46692,\"name\":\"DC'sLegendsofTomorrow\",\"permalink\":\"dcs-legends-of-tomorrow\",\"start_date\":\"2016-01-21\",\"end_date\":null,\"country\":\"US\",\"network\":\"TheCW\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/46692.jpg\"},{\"id\":24010,\"name\":\"TheWalkingDead\",\"permalink\":\"the-walking-dead\",\"start_date\":\"2010-10-31\",\"end_date\":null,\"country\":\"US\",\"network\":\"AMC+\",\"status\":\"Running\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/24010.jpg\"},{\"id\":46778,\"name\":\"StrangerThings\",\"permalink\":\"montauk\",\"start_date\":\"2016-07-15\",\"end_date\":null,\"country\":\"US\",\"network\":\"Netflix\",\"status\":\"Running\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/46778.jpg\"},{\"id\":47145,\"name\":\"DragonBallSuper\",\"permalink\":\"dragon-ball-super\",\"start_date\":\"2015-07-05\",\"end_date\":null,\"country\":\"JP\",\"network\":\"FujiTV\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/47145.\"},{\"id\":52439,\"name\":\"BokunoHeroAcademia\",\"permalink\":\"boku-no-hero-academia\",\"start_date\":\"2016-04-03\",\"end_date\":null,\"country\":\"JP\",\"network\":\"MBS\",\"status\":\"Running\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/52439.jpg\"},{\"id\":33514,\"name\":\"The100\",\"permalink\":\"the-100\",\"start_date\":\"2014-03-19\",\"end_date\":null,\"country\":\"US\",\"network\":\"TheCW\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/33514.jpg\"},{\"id\":22410,\"name\":\"Sherlock\",\"permalink\":\"sherlock\",\"start_date\":\"2010-07-25\",\"end_date\":null,\"country\":\"UK\",\"network\":\"BBCOne\",\"status\":\"ToBeDetermined\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/22410.jpg\"},{\"id\":5348,\"name\":\"Supernatural\",\"permalink\":\"supernatural\",\"start_date\":\"2005-09-13\",\"end_date\":null,\"country\":\"US\",\"network\":\"TheCW\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/5348.jpg\"},{\"id\":8362,\"name\":\"TheBigBangTheory\",\"permalink\":\"the-big-bang-theory\",\"start_date\":\"2007-09-24\",\"end_date\":null,\"country\":\"US\",\"network\":\"CBS\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/8362.jpg\"},{\"id\":31452,\"name\":\"Marvel'sAgentsofS.H.I.E.L.D.\",\"permalink\":\"marvel-s-agents-of-s-h-i-e-l-d\",\"start_date\":\"2013-09-24\",\"end_date\":null,\"country\":\"US\",\"network\":\"ABC\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/31452.jpg\"},{\"id\":37444,\"name\":\"Marvel'sDaredevil\",\"permalink\":\"daredevil\",\"start_date\":\"2015-04-10\",\"end_date\":null,\"country\":\"US\",\"network\":\"Netflix\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/37444.jpg\"},{\"id\":29977,\"name\":\"Vikings\",\"permalink\":\"vikings\",\"start_date\":\"2013-03-03\",\"end_date\":null,\"country\":\"CA\",\"network\":\"History\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/29977.jpg\"},{\"id\":29671,\"name\":\"Gotham\",\"permalink\":\"gotham\",\"start_date\":\"2014-09-22\",\"end_date\":null,\"country\":\"US\",\"network\":\"FOX\",\"status\":\"Ended\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/29671.png\"},{\"id\":36210,\"name\":\"Westworld\",\"permalink\":\"westworld\",\"start_date\":\"2016-10-02\",\"end_date\":null,\"country\":\"US\",\"network\":\"HBO\",\"status\":\"Running\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/36210.jpg\"},{\"id\":49269,\"name\":\"MiraculousLadyBug\",\"permalink\":\"miraculous-ladybug\",\"start_date\":\"2015-09-01\",\"end_date\":null,\"country\":\"FR\",\"network\":\"TF1\",\"status\":\"Running\",\"image_thumbnail_path\":\"https://static.episodate.com/images/tv-show/thumbnail/49269.jpg\"}]}";
            //var result1 = JsonConvert.DeserializeObject<ResponseTvShowModel>(json);
            //return true;
            
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<ResponseTvShowModel>(await response.Content.ReadAsStringAsync());
                        SearchPage++;
                        foreach (var item in result.TvShows)
                        {
                            var tvShowDetail = await GetTvShowsDetailsById(item.Id);
                            if(tvShowDetail != null)
                            {
                                await AddTvShows(tvShowDetail);
                                await AddTvShowsEpisodes(tvShowDetail.Episodes, item.Id);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }           

            return true;
        }

        private async Task<TvShowModel> GetTvShowsDetailsById(int idShow)
        {            
            var client = new HttpClient();
            var request = new HttpRequestMessage{Method = HttpMethod.Get, RequestUri = new Uri($"https://www.episodate.com/api/show-details?q={idShow}")};

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<TvShowModel>(await response.Content.ReadAsStringAsync());
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return new TvShowModel();
        }
    }
}

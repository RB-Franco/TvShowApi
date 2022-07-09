using Application.Interface;
using Domain.Interfaces;
using Entity.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Application
{
    public class ApplicationTvShow : IApplicationTvShow
    {
        private readonly ITvShow _ITvShow;
        public ApplicationTvShow(ITvShow ITvShow)
        {
            _ITvShow = ITvShow;
        }

        public async Task<IEnumerable<TvShow>> SearchAll()
        {
            return await _ITvShow.SearchAll();
        }

        public async Task<TvShow> SearchById(int id)
        {
            return await _ITvShow.SearchById(id);
        }

        public async Task<IEnumerable<TvShow>> SearchByName(string name)
        {
            return await _ITvShow.SearchByName(name);
        }

        public async Task<TvShow> AddTvShowToFavorites(string name)
        {
            return await _ITvShow.AddTvShowToFavorites(name);
        }

        public async Task<TvShow> RemoveTvShowToFavorites(string name)
        {
            return await _ITvShow.RemoveTvShowToFavorites(name);
        }

        public async Task<IEnumerable<Episode>> GetEpisodesByTvShow(string name)
        {
            return await _ITvShow.GetEpisodesByTvShow(name);
        }
    }
}

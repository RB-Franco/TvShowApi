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
        public async Task<List<TvShow>> SearchAll()
        {
            return await _ITvShow.SearchAll();
        }

        public async Task<TvShow> SearchById(int id)
        {
            return await _ITvShow.SearchById(id);
        }

        public async Task<List<TvShow>> SearchByName(string name)
        {
            return await _ITvShow.SearchByName(name);
        }
    }
}

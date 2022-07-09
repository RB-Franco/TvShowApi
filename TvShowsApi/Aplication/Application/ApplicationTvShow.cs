using Application.Interface;
using AutoMapper;
using Domain.Interfaces;
using Entity.Entity;
using Models.Models;
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

        public async Task<IEnumerable<TvShowModel>> GetAllTvShows()
        {
            var response = await _ITvShow.GetAllTvShows();
            return Mapper.Map<IEnumerable<TvShow>, IEnumerable<TvShowModel>>(response);
        }

        public async Task<IEnumerable<TvShowModel>> SearchByName(string name)
        {
            var response = await _ITvShow.SearchByName(name);
            return Mapper.Map<IEnumerable<TvShow>, IEnumerable<TvShowModel>>(response);
        }

        public async Task<IEnumerable<FavoriteModel>> GetAllFavoritesByUserId(string userId)
        {
            var response = await _ITvShow.GetAllFavoritesByUserId(userId);
            return Mapper.Map<IEnumerable<Favorite>, IEnumerable<FavoriteModel>>(response);
        }

        public async Task<FavoriteModel> AddTvShowToFavorites(TvShowModel tvShow, string userId)
        {

            var request = Mapper.Map<TvShowModel, TvShow>(tvShow);
            var response = await _ITvShow.AddTvShowToFavorites(request, userId);
            return Mapper.Map<Favorite, FavoriteModel>(response);
        }

        public async Task<bool> RemoveTvShowToFavorites(FavoriteModel favorite)
        {
            var request = Mapper.Map<FavoriteModel, Favorite>(favorite);
            return await _ITvShow.RemoveTvShowToFavorites(request);
        }

        public async Task<IEnumerable<EpisodeModel>> GetEpisodesByTvShowId(int tvShowId)
        {
            var response = await _ITvShow.GetEpisodesByTvShowId(tvShowId);
            return Mapper.Map< IEnumerable<Episode>, IEnumerable<EpisodeModel>>(response);
        }
    }
}

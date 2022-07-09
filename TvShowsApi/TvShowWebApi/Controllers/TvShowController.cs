using Application.Interface;
using Entity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TvShowWebApi.Models;

namespace TvShowWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvShowController : Controller
    {
        private readonly IApplicationTvShow _IApplicationTvShow;

        public TvShowController(IApplicationTvShow ApplicationTvShow)
        {
            _IApplicationTvShow = ApplicationTvShow;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetAllTvShows")]        
        public async Task<IActionResult> GetAllTvShows()
        {
            var result = await _IApplicationTvShow.SearchAll();
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetEpisodesByTvShowId")]
        public async Task<IActionResult> GetEpisodesByTvShowId([FromQuery] int tvShowId)
        {
            var result = await _IApplicationTvShow.GetEpisodesByTvShowId(tvShowId);
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/AddTvShowToFavorites")]
        public async Task<IActionResult> AddTvShowToFavorites([FromBody] TvShowModel tvShow)
        {

            var request = new TvShow
            {
                Id = tvShow.Id,
                ReferenceId = tvShow.ReferenceId
            };

            var result = await _IApplicationTvShow.AddTvShowToFavorites(request);
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpDelete("/api/RemoveTvShowToFavorites")]
        public async Task<IActionResult> RemoveTvShowToFavorites([FromBody] Favorites favorite)
        {
            var result = await _IApplicationTvShow.RemoveTvShowToFavorites(favorite);
            return Ok(result);
        }

    }
}

using Application.Interface;
using Entity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpGet("/api/getAllTvShows")]        
        public async Task<IActionResult> GetAllTvShows()
        {
            var result = await _IApplicationTvShow.GetAllTvShows();
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/getAllTvShowsByName")]
        public async Task<IActionResult> GetAllTvShowsByName([FromQuery] string name)
        {
            var result = await _IApplicationTvShow.GetAllTvShowsByName(name);
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/getAllTvShowsByGenere")]
        public async Task<IActionResult> GetAllTvShowsByGenere([FromQuery] string genere)
        {
            var result = await _IApplicationTvShow.GetAllTvShowsByGenere(genere);
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetTvShowDetailById")]
        public async Task<IActionResult> GetTvShowDetailById([FromQuery] int tvShowId)
        {
            var tvShow = await _IApplicationTvShow.GetTvShowById(tvShowId);
            var result = await _IApplicationTvShow.GetEpisodesByTvShowId(tvShowId);
            tvShow.Episodes = result.ToList();

            return Ok(tvShow);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/getAllFavoritesByUserId")]
        public async Task<IActionResult> GetAllFavoritesByUserId([FromQuery] string userId)
        {
            var result = await _IApplicationTvShow.GetAllFavoritesByUserId(userId);
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/addTvShowToFavorites")]
        public async Task<IActionResult> AddTvShowToFavorites([FromBody] TvShowModel tvShow, [FromQuery] string userId)
        {
            var result = await _IApplicationTvShow.AddTvShowToFavorites(tvShow, userId);
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpDelete("/api/removeTvShowToFavorites")]
        public async Task<IActionResult> RemoveTvShowToFavorites([FromBody] FavoriteModel favorite)
        {
            var result = await _IApplicationTvShow.RemoveTvShowToFavorites(favorite);
            return Ok(result);
        }

    }
}

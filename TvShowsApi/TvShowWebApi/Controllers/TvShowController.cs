using Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("/api/GetAllTvShows")]        
        public async Task<IActionResult> GetAllTvShows()
        {
            var result = await _IApplicationTvShow.SearchAll();
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetEpisodesByTvShow")]
        public async Task<IActionResult> GetEpisodesByTvShow(string name)
        {
            var result = await _IApplicationTvShow.GetEpisodesByTvShow(name);
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/AddTvShowToFavorites")]
        public async Task<IActionResult> AddTvShowToFavorites(string name)
        {
            var result = await _IApplicationTvShow.AddTvShowToFavorites(name);
            return Ok(result);
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/AddTvShowToFavorites")]
        public async Task<IActionResult> RemoveTvShowToFavorites(string name)
        {
            var result = await _IApplicationTvShow.RemoveTvShowToFavorites(name);
            return Ok(result);
        }

    }
}

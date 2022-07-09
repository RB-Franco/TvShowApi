using Application.Interface;
using Entity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using System.Linq;
using System.Threading.Tasks;
using TvShowWebApi.Models;
using TvShowWebApi.Token;

namespace TvShowWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUser _IApplicationUser;
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly TokenUser _CreateTokenUser;

        public UserController(IApplicationUser ApplicationUser, UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _IApplicationUser = ApplicationUser;
            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _CreateTokenUser = new TokenUser(_IApplicationUser, _UserManager, _SignInManager);
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateTokenIdentity")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] LoginModel login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return Unauthorized();


            var token = await _CreateTokenUser.ReturnTokenValidate(login);
            if (string.IsNullOrEmpty(token))
                return BadRequest("User don't exist in database");

            return Ok(token);
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUserIdentity")]
        public async Task<IActionResult> AddUserIdentity([FromBody] SignUpModel login)
        {
            if (string.IsNullOrWhiteSpace(login.Name) || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return BadRequest("There are some missing fields.");

            var user = new User
            {
                Name = login.Name,
                UserName = login.Email,
                Email = login.Email
            };

            var result = await _UserManager.CreateAsync(user, login.Password);
            if (result.Errors.Any())
                return BadRequest(result.Errors);

            if (!await _CreateTokenUser.SendEmailConfirmation(user))
                return BadRequest("Error confirming user.");

            return Ok("User added successfully.");

        }
    }
}

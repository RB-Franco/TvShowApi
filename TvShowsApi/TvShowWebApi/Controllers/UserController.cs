using Application.Interface;
using Entity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Text;
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

        public UserController(IApplicationUser ApplicationUser, UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _IApplicationUser = ApplicationUser;
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CreateTokenIdentity")]
        public async Task<IActionResult> CreateTokenIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return Unauthorized();

            var result = await _SignInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var idUser = await _IApplicationUser.ReturnIdUser(login.Email);

                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Rodrigo Barcelos Franco")
                    .AddIssuer("Teste.Securiry.Bearer")
                    .AddAudience("Teste.Securiry.Bearer")
                    .AddClaim("idUser", idUser)
                    .AddExpiry(30)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return BadRequest("User don't exist in database");
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUserIdentity")]
        public async Task<IActionResult> AddUserIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Name) || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return BadRequest("There are some missing filds.");


            var user = new User
            {
                Name = login.Name,
                UserName = login.Email,
                Email = login.Email
            };

            var result = await _UserManager.CreateAsync(user, login.Password);
            if (result.Errors.Any())
                return Ok(result.Errors);

            //Geração de confirmãção 
            var userId = await _UserManager.GetUserIdAsync(user);
            var code = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result2 = await _UserManager.ConfirmEmailAsync(user, code);

            if(result2.Succeeded)
                return Ok("User added successfully.");
            else
                return BadRequest("Error confirming  user.");
        }
    }
}

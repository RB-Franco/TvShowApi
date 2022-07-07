using Aplication.Interface;
using Entity.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
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
        private readonly IAplicationUser _IAplicationUser;
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public UserController(IAplicationUser AplicationUser, UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _IAplicationUser = AplicationUser;
            _UserManager = UserManager;
            _SignInManager = SignInManager;
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/CriarToken")]
        public async Task<IActionResult> CreateToken([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return Unauthorized();

            var result =await  _IAplicationUser.ValidateUser(login.Email, login.Password);
            if(result)
            {
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Rodrigo Barcelos Franco")
                    .AddIssuer("Teste.Securiry.Bearer")
                    .AddAudience("Teste.Securiry.Bearer")
                    .AddClaim("UserApiNumber", "1")
                    .AddExpiry(30)
                    .Builder();
                
                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUser")]
        public async Task<IActionResult> AddUser([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Name) || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return Ok("There are some missing filds.");

            var result = await _IAplicationUser.AddUser(login.Name, login.Email, login.Password);
            if (result)
                return Ok("User added successfully.");
            else
                return Ok("Error adding user.");
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
                var token = new TokenJWTBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                    .AddSubject("Rodrigo Barcelos Franco")
                    .AddIssuer("Teste.Securiry.Bearer")
                    .AddAudience("Teste.Securiry.Bearer")
                    .AddClaim("UserApiNumber", "1")
                    .AddExpiry(30)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Ok("User don't exist in database");
            }
        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("/api/AddUserIdentity")]
        public async Task<IActionResult> AddUserIdentity([FromBody] Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Name) || string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return Ok("There are some missing filds.");


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
                return Ok("Error confirming  user.");
        }


    }
}

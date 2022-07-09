using Application.Interface;
using Entity.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;
using TvShowWebApi.Models;

namespace TvShowWebApi.Token
{

    public class TokenUser
    {
        private readonly IApplicationUser _IApplicationUser;

        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;

        public TokenUser(IApplicationUser ApplicationUser, UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _IApplicationUser = ApplicationUser;

            _UserManager = UserManager;
            _SignInManager = SignInManager;

        }

        public async Task<string> ReturnTokenValidate(LoginModel login)
        {
            var result = await _SignInManager.PasswordSignInAsync(login.Email, login.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
                return string.Empty;


            var token = await GenerateToken(login.Email);
            return token;
        }

        private async Task<string> GenerateToken(string emails)
        {
            var idUser = await _IApplicationUser.ReturnIdUser(emails);
            var token = new TokenJWTBuilder()
                .AddSecurityKey(JwtSecurityKey.Create("Secret_Key-12345678"))
                .AddSubject("Rodrigo Barcelos Franco")
                .AddIssuer("Teste.Securiry.Bearer")
                .AddAudience("Teste.Securiry.Bearer")
                .AddClaim("idUser", idUser)
                .AddExpiry(30)
                .Builder();

            return token.value;
        }
        public async Task<bool> SendEmailConfirmation(User user)
        {
            if (await SendConfirmation(user))
                return true;

            return false;
        }

        private async Task<bool> SendConfirmation(User user)
        {
            var userId = await _UserManager.GetUserIdAsync(user);
            var code = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));


            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _UserManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
                return true;

            return false;
        }
    }
}

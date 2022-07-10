using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using TvShowWebApi;
using System.Threading.Tasks;
using System.Net;

namespace TvShowTest.Controllers
{
    public class UserControllerTest
    {        
        private readonly HttpClient _client;

        public UserControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("Get")]
        public async Task UserControllerTestAsync (string method)
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/UserController");
            var response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

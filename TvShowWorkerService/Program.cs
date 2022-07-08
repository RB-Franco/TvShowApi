using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TvShowWorkerService.Domain.Interface;
using TvShowWorkerService.Infrastructure.Repository;

namespace TvShowWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddSingleton<IWorkerTvShow, WorkerTvShowRepository>();
                });
    }
}

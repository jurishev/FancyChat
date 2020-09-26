using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Server
{
    /// <summary>
    /// ASP.NET Core generic host.
    /// </summary>
    public static class WebHost
    {
        private static readonly CancellationTokenSource cancelSource = new CancellationTokenSource();
        private static readonly CancellationToken cancelToken = cancelSource.Token;

        private static IHost host;

        public static void RunAsync()
        {
            host = CreateHostBuilder().Build();
            Task.Run(() => host.Run(), cancelToken);
        }

        public static void Stop()
        {
            if (host != null)
            {
                cancelSource.Cancel();
            }
        }

        public static void Dispose() => host?.Dispose();

        private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:5000");
                    webBuilder.UseStartup<Startup>();
                });
    }
}

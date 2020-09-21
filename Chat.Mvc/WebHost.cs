using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Mvc
{
    /// <summary>
    /// ASP.NET Core generic host.
    /// </summary>
    internal static class WebHost
    {
        private static readonly CancellationTokenSource cancelSource = new CancellationTokenSource();
        private static readonly CancellationToken cancelToken = cancelSource.Token;

        private static IHost host;

        public static void RunAsync()
        {
            host = CreateHostBuilder().Build();
            Task.Factory.StartNew(_ => host.Run(), TaskCreationOptions.LongRunning, cancelToken);
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
                    webBuilder.UseStartup<Startup>();
                });
    }
}

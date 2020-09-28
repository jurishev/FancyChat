using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SqlClient;
using Chat.Data;
using Chat.Data.AdoNet;

namespace Chat.Server
{
    public class Startup
    {
        public Startup()
        {
            DbConfiguration = new ConfigurationBuilder().AddJsonFile("dbconfig.json").Build();
        }

        public IConfiguration DbConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();

            services.AddScoped<IUserService, UserAdoNetService>();
            services.AddScoped(_ =>
            {
                var connection = new SqlConnection(DbConfiguration["ChatDbConnectionString"]);
                connection.Open();
                return connection;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });

            //app.UseSpa(spa =>
            //{
            //    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
            //});
        }
    }
}

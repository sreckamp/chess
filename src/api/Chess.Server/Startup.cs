using System.IO;
using Chess.Server.Model;
using Chess.Server.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chess.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        // ReSharper disable once MemberCanBePrivate.Global
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("chessCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddControllers();

            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = Path.Join(Directory.GetCurrentDirectory(), @"..\..\angular\dist\angular");
            });

            services.AddSignalR();
            services.AddSingleton<IGameProviderService, FileGameProviderService>();
            services.AddSingleton<IGameTranslator, GameTranslator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("chessCORS");

            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapHub<GameEventHub>("/api/events"); });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseWhen(context => HttpMethods.IsGet(context.Request.Method), builder =>
            {
                builder.UseSpa(spa => { });
            });
        }
    }
}

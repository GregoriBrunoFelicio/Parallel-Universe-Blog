using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Universo.Paralello.Blog.Api.Data;
using Universo.Paralello.Blog.Api.Data.Repositories;
using Universo.Paralello.Blog.Api.Services;
using Universo.Paralello.Blog.Api.Shared;

namespace Universo.Paralello.Blog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<UniversoParalelloBlogContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies()); ;

            Swagger.Configure(services);
            Ioc.RegisterServices(services);
            Authentication.Configure(services, Configuration.GetSection("TokenConfiguration").GetSection("Key").Value);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Universo.Paralello.Blog.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

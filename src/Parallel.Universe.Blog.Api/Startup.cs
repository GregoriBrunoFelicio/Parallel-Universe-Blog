using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parallel.Universe.Blog.Api.Configurations;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Shared;

namespace Parallel.Universe.Blog.Api
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
            services.AddControllers().ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<ParallelUniverseBlogContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .UseLazyLoadingProxies());

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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parallel.Universe.Blog.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
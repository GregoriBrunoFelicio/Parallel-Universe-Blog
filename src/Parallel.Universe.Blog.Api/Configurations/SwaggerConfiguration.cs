using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Parallel.Universe.Blog.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services) => services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Parallel.Universe.Blog.Api", Version = "v1" }));

        public static void UseSwaggerConfigurations(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parallel.Universe.Blog.Api v1"));
        }
    }
}

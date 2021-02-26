using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Parallel.Universe.Blog.Api.Shared
{
    public static class Swagger
    {
        public static void Configure(IServiceCollection services) => services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Universo.Paralello.Blog.Api", Version = "v1" }));
    }
}

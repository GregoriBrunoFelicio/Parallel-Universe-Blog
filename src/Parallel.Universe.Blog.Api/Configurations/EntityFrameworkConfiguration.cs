using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parallel.Universe.Blog.Api.Data;

namespace Parallel.Universe.Blog.Api.Configurations
{
    public static class EntityFrameworkConfiguration
    {
        public static void ConfigureEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ParallelUniverseBlogContext>(x =>
                        x.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                            .UseLazyLoadingProxies());
            services.AddApplicationInsightsTelemetry();
        }
    }
}

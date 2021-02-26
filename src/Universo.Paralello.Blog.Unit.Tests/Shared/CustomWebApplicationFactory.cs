using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parallel.Universe.Blog.Api;
using Parallel.Universe.Blog.Api.Data;

namespace Parallel.Universe.Blog.Tests.Shared
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<ParallelUniverseBlogContext>(configuracao =>
                {
                    configuracao.UseInMemoryDatabase("TestsDb");
                    configuracao.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var appDb = scopedServices.GetRequiredService<ParallelUniverseBlogContext>();
                appDb.Database.EnsureCreated();
            });
        }
    }
}

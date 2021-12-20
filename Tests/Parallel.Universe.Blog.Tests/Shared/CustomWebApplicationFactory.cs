using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Parallel.Universe.Blog.Api;
using Parallel.Universe.Blog.Api.Data;
using System.Linq;

namespace Parallel.Universe.Blog.Tests.Shared
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
          {
              var descriptor = services.SingleOrDefault(
                  d => d.ServiceType ==
                      typeof(DbContextOptions<ParallelUniverseBlogContext>));

              services.Remove(descriptor);

              services.AddDbContext<ParallelUniverseBlogContext>(options =>
              {
                  options.UseInMemoryDatabase("InMemoryDbForTesting");
              });

              var sp = services.BuildServiceProvider();

              using var scope = sp.CreateScope();
              var scopedServices = scope.ServiceProvider;
              var db = scopedServices.GetRequiredService<ParallelUniverseBlogContext>();
              db.Database.EnsureCreated();
          });
        }
    }
}

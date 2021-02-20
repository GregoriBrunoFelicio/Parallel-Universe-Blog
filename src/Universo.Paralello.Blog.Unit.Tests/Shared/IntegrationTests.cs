using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Universo.Paralello.Blog.Api;
using Universo.Paralello.Blog.Api.Data;

namespace Universo.Paralello.Blog.Tests.Shared
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

                services.AddDbContext<UniversoParalelloBlogContext>(configuracao =>
                {
                    configuracao.UseInMemoryDatabase("TestsDb");
                    configuracao.UseInternalServiceProvider(serviceProvider);

                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var appDb = scopedServices.GetRequiredService<UniversoParalelloBlogContext>();
                appDb.Database.EnsureCreated();
            });
        }
    }
}

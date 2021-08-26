using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data;
using System.Net.Http;

namespace Parallel.Universe.Blog.Tests.Shared
{
    public class IntegrationBase
    {
        protected HttpClient Client;
        protected ParallelUniverseBlogContext Context;

        [OneTimeSetUp]
        public void SetUp()
        {
            var factory = new CustomWebApplicationFactory();
            Context = factory.Services.CreateScope().ServiceProvider.GetService<ParallelUniverseBlogContext>();
            Client = factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services => services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>())).CreateClient();
        }
    }
}

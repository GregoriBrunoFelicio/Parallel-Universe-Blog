using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data;
using Xunit;

namespace Parallel.Universe.Blog.Tests.Shared
{
    public class Integration : IClassFixture<CustomWebApplicationFactory>
    {
        protected HttpClient Client;
        protected ParallelUniverseBlogContext Context;

        [OneTimeSetUp]
        public void SetUp()
        {
            var factory = new CustomWebApplicationFactory();
            Client = factory.CreateClient();
            Context = factory.Services.CreateScope().ServiceProvider.GetService<ParallelUniverseBlogContext>();
        }
    }
}

using System.Net.Http;
using NUnit.Framework;
using Xunit;

namespace Parallel.Universe.Blog.Tests.Shared
{
    public class Integration : IClassFixture<CustomWebApplicationFactory>
    {
        protected HttpClient Client;

        [OneTimeSetUp]
        public void SetUp()
        {
            var factory = new CustomWebApplicationFactory();
            Client = factory.CreateClient();
        }
    }

}

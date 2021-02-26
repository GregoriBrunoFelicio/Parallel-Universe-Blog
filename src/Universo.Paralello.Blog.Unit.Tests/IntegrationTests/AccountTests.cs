using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Parallel.Universe.Blog.Tests.Shared;
using Parallel.Universe.Blog.Tests.Shared.Builders;

namespace Parallel.Universe.Blog.Tests.IntegrationTests
{
    public class AccountTests : Integration
    {
        protected UserViewModelBuilder UserViewModelBuilder;

        [SetUp]
        public new void SetUp() => UserViewModelBuilder = new UserViewModelBuilder();
    }

    public class CreateTests : AccountTests
    {
        [Test]
        public async Task DeveRetornarOk()
        {
            var model = UserViewModelBuilder.WithAccount().Generate();
            var response = await Client.PostAsJsonAsync("Account/Create", model);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

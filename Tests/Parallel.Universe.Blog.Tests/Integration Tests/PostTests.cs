using FluentAssertions;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Tests.Shared;
using Parallel.Universe.Blog.Tests.Shared.Builders;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Tests.Integration_Tests
{
    public class PostTests : IntegrationBase
    {
        protected PostViewModelBuilder postViewModelBuilder;
        protected UserRepository userRepository;

        [OneTimeSetUp]
        public new void SetUp()
        {

            postViewModelBuilder = new PostViewModelBuilder();
            userRepository = new UserRepository(Context);
        }
    }

    public class PostCreateTests : PostTests
    {
        [Test]
        public async Task ShouldReturnOk()
        {
            var user = new UserBuilder().WithActive(true).Generate();
            await userRepository.AddAsync(user);
            await Context.SaveChangesAsync();

            var model = postViewModelBuilder.Generate();
            model.UserId = user.Id;
            var response = await Client.PostAsJsonAsync("Post", model);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

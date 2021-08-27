using FluentAssertions;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Tests.Shared;
using Parallel.Universe.Blog.Tests.Shared.Builders.Models;
using Parallel.Universe.Blog.Tests.Shared.Builders.ViewModels;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Tests.Integration_Tests
{
    public class PostTests : IntegrationBase
    {
        protected PostViewModelBuilder postViewModelBuilder;
        protected PostBuilder postBuilder;
        protected UserBuilder userBuilder;
        protected IUserRepository userRepository;
        protected IPostRepository postRepository;
        protected IUnitOfWork unitOfWork;


        [OneTimeSetUp]
        public new void SetUp()
        {
            postViewModelBuilder = new PostViewModelBuilder();
            postBuilder = new PostBuilder();
            userBuilder = new UserBuilder();
            userRepository = new UserRepository(Context);
            postRepository = new PostRepository(Context);
            unitOfWork = new UnitOfWork(Context);
        }
    }

    public class PostCreateTests : PostTests
    {
        [Test]
        public async Task ShouldReturnOk()
        {
            var user = userBuilder.WithActive(true).Generate();
            await userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();

            var model = postViewModelBuilder
                .WithUserId(user.Id)
                .WithActive(true)
                .Generate();

            var response = await Client.PostAsJsonAsync("Post", model);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

    public class PostGetById : PostTests
    {
        [Test]
        public async Task ShouldReturnOk()
        {
            var user = new UserBuilder().WithActive(true).Generate();
            await userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();

            var post = postBuilder.WithUserId(user.Id).Generate();
            await postRepository.AddAsync(post);
            await unitOfWork.CommitAsync();

            var response = await Client.GetAsync($"Post/{post.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

    public class PostGetAllActive : PostTests
    {
        [Test]
        public async Task ShouldReturnOk()
        {
            var user = new UserBuilder().WithActive(true).Generate();
            await userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();

            var post = postBuilder.WithUserId(user.Id).Generate();
            await postRepository.AddAsync(post);
            await unitOfWork.CommitAsync();

            var response = await Client.GetAsync("Post/AllActive");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

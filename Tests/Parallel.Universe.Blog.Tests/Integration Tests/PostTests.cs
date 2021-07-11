using FluentAssertions;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Tests.Shared;
using Parallel.Universe.Blog.Tests.Shared.Builders;
using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Tests.Integration_Tests
{
    public class PostTests : IntegrationBase
    {
        protected PostViewModelBuilder postViewModelBuilder;
        protected PostBuilder postBuilder;
        protected IUserRepository userRepository;
        protected IPostRepository postRepository;
        protected IUnitOfWork unitOfWork;


        [OneTimeSetUp]
        public new void SetUp()
        {

            postViewModelBuilder = new PostViewModelBuilder();
            postBuilder = new PostBuilder();
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
            var user = new UserBuilder().WithActive(true).Generate();
            await userRepository.AddAsync(user);
            await Context.SaveChangesAsync();

            var model = postViewModelBuilder.Generate();
            model.UserId = user.Id;
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

            var post = new Post(0, "Title", "Description", "Text", DateTime.Now, true, user.Id);
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

            var post = new Post(0, "Title", "Description", "Text", DateTime.Now, true, user.Id);
            await postRepository.AddAsync(post);
            await unitOfWork.CommitAsync();

            var response = await Client.GetAsync("Post/AllActive");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

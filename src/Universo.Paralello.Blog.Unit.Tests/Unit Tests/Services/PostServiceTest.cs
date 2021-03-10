using AutoMapper;
using Moq;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Services;
using Parallel.Universe.Blog.Api.Services.Results;
using Parallel.Universe.Blog.Api.ViewModels;
using Parallel.Universe.Blog.Tests.Shared.Builders;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Tests.Unit_Tests.Services
{
    public class PostServiceTest
    {
        protected Mock<IPostRepository> postRepositoryMock;
        protected Mock<IUserRepository> userRepositoryMock;
        protected Mock<IMapper> mapperMock;
        protected PostService postService;
        protected PostViewModelBuilder postViewModelBuilder;
        protected PostBuilder postBuilder;

        [OneTimeSetUp]
        public void SetUp()
        {
            postRepositoryMock = new Mock<IPostRepository>();
            userRepositoryMock = new Mock<IUserRepository>();
            mapperMock = new Mock<IMapper>();
            postViewModelBuilder = new PostViewModelBuilder();
            postBuilder = new PostBuilder();

            postService = new PostService(postRepositoryMock.Object, userRepositoryMock.Object, mapperMock.Object);
        }
    }

    public class PostCreateTests : PostServiceTest
    {

        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var post = postBuilder.Generate();
            var model = postViewModelBuilder.WithUserId(1).WithActive(true).Generate();

            mapperMock.Setup(x => x.Map<Post>(model)).Returns(post);
            result = await postService.Create(model);
        }

        [Test]
        public void ShouldCallMapper() => mapperMock.Verify(
            x => x.Map<Post>(It.IsAny<PostViewModel>()), Times.Once);
    }
}

﻿using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Services;
using Parallel.Universe.Blog.Api.Services.Results;
using Parallel.Universe.Blog.Api.ViewModels;
using Parallel.Universe.Blog.Tests.Shared.Builders.Models;
using Parallel.Universe.Blog.Tests.Shared.Builders.ViewModels;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Tests.Unit_Tests.Services
{
    public class PostServiceTest
    {
        protected Mock<IPostRepository> postRepositoryMock;
        protected Mock<IUserRepository> userRepositoryMock;
        protected Mock<IMapper> mapperMock;
        protected Mock<IUnitOfWork> unitOfWorkMock;
        protected PostService postService;
        protected PostViewModelBuilder postViewModelBuilder;
        protected PostBuilder postBuilder;
        protected UserBuilder userBuilder;

        [OneTimeSetUp]
        public void SetUp()
        {
            postRepositoryMock = new Mock<IPostRepository>();
            userRepositoryMock = new Mock<IUserRepository>();
            mapperMock = new Mock<IMapper>();
            unitOfWorkMock = new Mock<IUnitOfWork>();
            postViewModelBuilder = new PostViewModelBuilder();
            postBuilder = new PostBuilder();
            userBuilder = new UserBuilder();
            postService = new PostService(postRepositoryMock.Object, userRepositoryMock.Object, mapperMock.Object, unitOfWorkMock.Object);
        }
    }

    public class PostServiceCreateTests : PostServiceTest
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var post = postBuilder.Generate();
            var model = postViewModelBuilder.WithUserId(1).WithActive(true).Generate();
            var user = userBuilder.WithActive(true).Generate();

            mapperMock.Setup(x => x.Map<Post>(model)).Returns(post);
            userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            result = await postService.Create(model);
        }

        [Test]
        public void ShouldCallMapper() => mapperMock.Verify(
            x => x.Map<Post>(It.IsAny<PostViewModel>()), Times.Once);

        [Test]
        public void ShouldCallMethodGetByIdAsync() =>
            userRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);

        [Test]
        public void ShouldCallMethodAddAsync() =>
            postRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Post>()), Times.Once);

        [Test]
        public void ShouldReturnTheCorrectResultMessage() =>
            result.Message.Should().Be("Post created successfully.");
    }

    public class PostServiceCreateUserNotFoundTests : PostServiceTest
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var post = postBuilder.Generate();
            var model = postViewModelBuilder.WithUserId(1).WithActive(true).Generate();

            mapperMock.Setup(x => x.Map<Post>(model)).Returns(post);
            userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(value: null);
            result = await postService.Create(model);
        }

        [Test]
        public void ShouldReturnTheCorrectResultMessage() =>
                    result.Message.Should().Be("User not found.");
    }

    public class PostServiceCreateInactiveAccount : PostServiceTest
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var post = postBuilder.Generate();
            var model = postViewModelBuilder.Generate();
            var user = userBuilder.WithActive(false).Generate();

            mapperMock.Setup(x => x.Map<Post>(model)).Returns(post);
            userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            result = await postService.Create(model);
        }

        [Test]
        public void ShouldReturnTheCorrectResultMessage() =>
            result.Message.Should().Be("Inactive account.");
    }

    public class PostUpdateTests : PostServiceTest
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var model = postViewModelBuilder.WithActive(true).WithUserId(1).Generate();
            var user = userBuilder.WithActive(true).WithId(1).Generate();
            var post = postBuilder.WithUserId(user.Id).Generate();

            mapperMock.Setup(x => x.Map<Post>(model)).Returns(post);
            userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            postRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(post);
            unitOfWorkMock.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            result = await postService.Update(model);
        }

        [Test]
        public void ShouldCallMapper() => mapperMock.Verify(
            x => x.Map<Post>(It.IsAny<PostViewModel>()), Times.Once);

        [Test]
        public void ShouldCallMethodGetByIdAsync() =>
            userRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);

        [Test]
        public void ShouldCallMethodUpdateAsync() =>
            postRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Post>()), Times.Once);

        [Test]
        public void ShouldCAllMethodCommigAsync() =>
            unitOfWorkMock.Verify(x => x.CommitAsync(), Times.Once);

        [Test]
        public void ShouldReturnTheCorrectResultMessage() =>
            result.Message.Should().Be("Post updated successfully.");
    }


    public class PostUpdateNotFoundUserTests : PostServiceTest
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var model = postViewModelBuilder.WithUserId(1).WithActive(true).Generate();
            result = await postService.Update(model);
        }

        [Test]
        public void ShouldReturnTheCorrectResultMessage() =>
                   result.Message.Should().Be("User not found.");
    }

    public class PostUpdateInactiveAccountTests : PostServiceTest
    {
        private IResult result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var model = postViewModelBuilder.WithUserId(1).WithActive(true).Generate();
            var user = userBuilder.WithActive(false).Generate();
            var post = postBuilder.WithUserId(user.Id).Generate();
            var oldPost = postBuilder.WithUserId(user.Id).Generate();

            mapperMock.Setup(x => x.Map<Post>(model)).Returns(post);
            userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
            postRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(oldPost);
            result = await postService.Update(model);
        }

        [Test]
        public void ShouldReturnTheCorrectResultMessage() =>
            result.Message.Should().Be("Inactive account.");
    }
}

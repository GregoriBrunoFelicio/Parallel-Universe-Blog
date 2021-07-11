using AutoBogus;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Services;
using Parallel.Universe.Blog.Api.Services.Results;
using Parallel.Universe.Blog.Api.ViewModels;
using Parallel.Universe.Blog.Tests.Shared.Builders;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Tests.Unit_Tests.Services
{
    public class AccountServiceTests
    {
        protected Mock<IUserRepository> userRepositoryMock;
        protected Mock<IAccountRepository> accountRepositoryMock;
        protected Mock<ITokenService> tokenServiceMock;
        protected Mock<IMapper> mapperMock;
        protected Mock<IUnitOfWork> unitOfWorkMock;
        protected IAccountService accountService;
        protected UserBuilder userBuilder;
        protected AccountBuilder accountBuilder;
        protected UserViewModelBuilder userViewModelBuilder;
        protected AccountInputModelBuilder accountViewModelBuilder;

        [OneTimeSetUp]
        public void SetUp()
        {
            userRepositoryMock = new Mock<IUserRepository>();
            accountRepositoryMock = new Mock<IAccountRepository>();
            tokenServiceMock = new Mock<ITokenService>();
            mapperMock = new Mock<IMapper>();
            unitOfWorkMock = new Mock<IUnitOfWork>();

            userBuilder = new UserBuilder();
            accountBuilder = new AccountBuilder();
            userViewModelBuilder = new UserViewModelBuilder();
            accountViewModelBuilder = new AccountInputModelBuilder();

            accountService = new AccountService(
                userRepositoryMock.Object,
                accountRepositoryMock.Object,
                tokenServiceMock.Object,
                mapperMock.Object,
                unitOfWorkMock.Object);
        }
    }

    public class AccountCreateTests : AccountServiceTests
    {
        private IResult _result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var model = userViewModelBuilder.WithAccount().Generate();
            var user = userBuilder.Generate();
            mapperMock.Setup(x => x.Map<UserViewModel, User>(model)).Returns(user);
            _result = await accountService.Create(model);
        }

        [Test]
        public void ShouldCallMethodGetByEmailAsync() => accountRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once);

        [Test]
        public void ShouldCallMapper() =>
            mapperMock.Verify(x => x.Map<UserViewModel, User>(It.IsAny<UserViewModel>()), Times.Once);

        [Test]
        public void ShouldCallMethodAddAsync() => userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);

        [Test]
        public void ShouldReturnTheCorrectMessage() =>
            _result.Message.Should().Be("User created successfully.");

        [Test]
        public void OResultadoDeveRetornarSucessoVerdadeiro() => _result.Success.Should().BeTrue();
    }

    public class CreateWhenEmailIsAlreadyRegistered : AccountServiceTests
    {
        private IResult _result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var model = userViewModelBuilder.WithAccount().Generate();
            accountRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(new Account());
            _result = await accountService.Create(model);
        }

        [Test]
        public void ShouldReturnTheCorrectMessage() => _result.Message.Should().Be("E-mail already registred.");

        [Test]
        public void ShouldReturnFalse() => _result.Success.Should().BeFalse();
    }

    public class VerifyTests : AccountServiceTests
    {
        private ILoginResult _result;
        private LoginInputModel _model;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var account = accountBuilder.Generate();
            var user = userBuilder.WithActive(true).Generate();
            account.SetUser(user);
            _model = new LoginInputModel { Password = account.Password.Value };

            account.Password.Encrypt();

            accountRepositoryMock.Setup(x => x.GetByEmailAsync(_model.Email)).ReturnsAsync(account);
            tokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("token");

            _result = await accountService.Verify(_model);
        }

        [Test]
        public void ShouldCallMethodGetByEmailAsync() =>
            accountRepositoryMock.Verify(x => x.GetByEmailAsync(_model.Email), Times.Once);

        [Test]
        public void ShouldCallMethodGenerateToken() =>
                   tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Once);

        [Test]
        public void ShouldReturnTheCorrectMessage() => _result.Message.Should().Be("Login successfully.");

        [Test]
        public void ShouldReturnTrue() => _result.Success.Should().BeTrue();

        [Test]
        public void TheTokenShouldNotBeNull() => _result.Token.Should().NotBeEmpty();
    }

    public class VerifyWhenAccountIsNotFound : AccountServiceTests
    {
        private ILoginResult _result;
        private LoginInputModel _model;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            _model = new AutoFaker<LoginInputModel>().Generate();
            accountRepositoryMock.Setup(x => x.GetByEmailAsync(_model.Email)).ReturnsAsync(value: null);
            _result = await accountService.Verify(_model);
        }

        [Test]
        public void ShouldCallMethodGetByEmailAsync() =>
                accountRepositoryMock.Verify(x => x.GetByEmailAsync(_model.Email), Times.Once);

        [Test]
        public void ShouldReturnTheCorrectMessage() => _result.Message.Should().Be("Invalid email or password.");
    }

    public class VerifyWhenPasswordIsInvalid : AccountServiceTests
    {
        private ILoginResult _result;
        private LoginInputModel _model;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var account = accountBuilder.Generate();
            _model = new AutoFaker<LoginInputModel>().Generate();
            account.Password.Encrypt();
            accountRepositoryMock.Setup(x => x.GetByEmailAsync(_model.Email)).ReturnsAsync(account);
            _result = await accountService.Verify(_model);
        }

        [Test]
        public void ShouldCallMethodGetByEmailAsync() =>
            accountRepositoryMock.Verify(x => x.GetByEmailAsync(_model.Email), Times.Once);

        [Test]
        public void ShouldReturnTheCorrectMessage() => _result.Message.Should().Be("Invalid email or password.");
    }
}

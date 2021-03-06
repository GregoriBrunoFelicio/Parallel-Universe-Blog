using AutoBogus;
using AutoMapper;
using FluentAssertions;
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
    public class AccountServiceTests
    {
        protected Mock<IUserRepository> UserRepositoryMock;
        protected Mock<IAccountRepository> AccountRepositoryMock;
        protected Mock<ITokenService> TokenServiceMock;
        protected Mock<IMapper> MapperMock;
        protected IAccountService AccountService;
        protected UserBuilder UserBuilder;
        protected AccountBuilder AccountBuilder;
        protected UserViewModelBuilder UserViewModelBuilder;
        protected AccountViewModelBuilder AccountViewModelBuilder;

        [OneTimeSetUp]
        public void SetUp()
        {
            UserRepositoryMock = new Mock<IUserRepository>();
            AccountRepositoryMock = new Mock<IAccountRepository>();
            TokenServiceMock = new Mock<ITokenService>();
            MapperMock = new Mock<IMapper>();

            UserBuilder = new UserBuilder();
            AccountBuilder = new AccountBuilder();
            UserViewModelBuilder = new UserViewModelBuilder();
            AccountViewModelBuilder = new AccountViewModelBuilder();

            AccountService = new AccountService(
                UserRepositoryMock.Object,
                AccountRepositoryMock.Object,
                TokenServiceMock.Object,
                MapperMock.Object);
        }
    }

    public class CreateTests : AccountServiceTests
    {
        private IResult _result;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var model = UserViewModelBuilder.WithAccount().Generate();
            var user = UserBuilder.Generate();
            MapperMock.Setup(x => x.Map<UserViewModel, User>(model)).Returns(user);
            _result = await AccountService.Create(model);
        }

        [Test]
        public void ShouldCallMethodGetByEmailAsync() => AccountRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once);

        [Test]
        public void ShouldCallMethodMap() =>
            MapperMock.Verify(x => x.Map<UserViewModel, User>(It.IsAny<UserViewModel>()), Times.Once);

        [Test]
        public void ShouldCallMethodAddAsync() => UserRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);

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
            var model = UserViewModelBuilder.WithAccount().Generate();
            AccountRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(new Account());
            _result = await AccountService.Create(model);
        }

        [Test]
        public void ShouldReturnTheCorrectMessage() => _result.Message.Should().Be("E-mail already registred.");

        [Test]
        public void ShouldReturnFalse() => _result.Success.Should().BeFalse();
    }

    public class VerifyTests : AccountServiceTests
    {
        private ILoginResult _result;
        private LoginViewModel _model;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var account = AccountBuilder.Generate();
            account.User = UserBuilder.WithActive(true).Generate();
            _model = new LoginViewModel { Password = account.Password.Value };

            account.Password.Encrypt();

            AccountRepositoryMock.Setup(x => x.GetByEmailAsync(_model.Email)).ReturnsAsync(account);
            TokenServiceMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("token");

            _result = await AccountService.Verify(_model);
        }

        [Test]
        public void ShouldCallMethodGetByEmailAsync() =>
            AccountRepositoryMock.Verify(x => x.GetByEmailAsync(_model.Email), Times.Once);

        [Test]
        public void ShouldCallMethodGenerateToken() =>
                   TokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Once);

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
        private LoginViewModel _model;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            _model = new AutoFaker<LoginViewModel>().Generate();
            AccountRepositoryMock.Setup(x => x.GetByEmailAsync(_model.Email)).ReturnsAsync(value: null);
            _result = await AccountService.Verify(_model);
        }

        [Test]
        public void ShouldCallMethodGetByEmailAsync() =>
                AccountRepositoryMock.Verify(x => x.GetByEmailAsync(_model.Email), Times.Once);

        [Test]
        public void ShouldReturnTheCorrectMessage() => _result.Message.Should().Be("Invalid email or password.");
    }

    public class VerifyWhenPasswordIsInvalid : AccountServiceTests
    {
        private ILoginResult _result;
        private LoginViewModel _model;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var account = AccountBuilder.Generate();
            _model = new AutoFaker<LoginViewModel>().Generate();
            account.Password.Encrypt();
            AccountRepositoryMock.Setup(x => x.GetByEmailAsync(_model.Email)).ReturnsAsync(account);
            _result = await AccountService.Verify(_model);
        }

        [Test]
        public void ShouldCallMethodGetByEmailAsync() =>
            AccountRepositoryMock.Verify(x => x.GetByEmailAsync(_model.Email), Times.Once);

        [Test]
        public void ShouldReturnTheCorrectMessage() => _result.Message.Should().Be("Invalid email or password.");
    }
}

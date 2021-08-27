using FluentAssertions;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.ViewModels;
using Parallel.Universe.Blog.Tests.Shared;
using Parallel.Universe.Blog.Tests.Shared.Builders.Models;
using Parallel.Universe.Blog.Tests.Shared.Builders.ViewModels;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Tests.Integration_Tests
{
    public class AccountTests : IntegrationBase
    {
        protected UserViewModelBuilder UserViewModelBuilder;
        protected UserBuilder UserBuilder;
        protected IUserRepository userRepository;
        protected IUnitOfWork unitOfWork;

        [OneTimeSetUp]
        public new void SetUp()
        {
            UserViewModelBuilder = new UserViewModelBuilder();
            UserBuilder = new UserBuilder();
            userRepository = new UserRepository(Context);
            unitOfWork = new UnitOfWork(Context);
        }
    }

    public class AccountCreateTests : AccountTests
    {
        [Test]
        public async Task ShouldReturnOk()
        {
            var model = UserViewModelBuilder.WithAccount().Generate();
            var response = await Client.PostAsJsonAsync("Account/Create", model);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task ShouldReturnBadRequest()
        {
            var model = UserViewModelBuilder.Generate();

            var response = await Client.PostAsJsonAsync("Account/Create", model);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }

    public class LoginTests : AccountTests
    {
        [Test]
        public async Task ShouldReturnOk()
        {
            var user = new UserBuilder().WithActive(true).Generate();

            var password = user.Account.Password.Value;

            user.Account.Password.Encrypt();

            await userRepository.AddAsync(user);
            await unitOfWork.CommitAsync();

            var model = new LoginInputModel
            {
                Email = user.Account.Email,
                Password = password
            };

            var response = await Client.PostAsJsonAsync("Account/Login", model);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

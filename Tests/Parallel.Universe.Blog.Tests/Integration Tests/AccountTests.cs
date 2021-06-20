using FluentAssertions;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.ViewModels;
using Parallel.Universe.Blog.Tests.Shared;
using Parallel.Universe.Blog.Tests.Shared.Builders;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Tests.Integration_Tests
{
    public class AccountTests : IntegrationBase
    {
        protected UserViewModelBuilder UserViewModelBuilder;
        protected UserBuilder UserBuilder;
        protected UserRepository userRepository;

        [OneTimeSetUp]
        public new void SetUp()
        {

            UserViewModelBuilder = new UserViewModelBuilder();
            UserBuilder = new UserBuilder();
            userRepository = new UserRepository(Context);

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
            var user = UserBuilder.Generate();

            var model = new UserViewModel
            {
                Name = user.Name,
                Active = true,
                Account = new AccountInputModel
                {
                    Email = user.Account.Email,
                    Password = user.Account.Password.Value,
                    PasswordConfirmation = user.Account.Password.Value
                }
            };

            await userRepository.AddAsync(user);
            await Context.SaveChangesAsync();

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
            await Context.SaveChangesAsync();

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

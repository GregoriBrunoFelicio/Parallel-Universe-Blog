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
    public class UserTests : IntegrationBase
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

    public class UpdateTests : UserTests
    {
        [Test]
        public async Task ShouldReturnOk()
        {
            var user = UserBuilder.Generate();

            await userRepository.AddAsync(user);
            await Context.SaveChangesAsync();

            var model = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Active = true,
                Account = new AccountInputModel
                {
                    Email = user.Account.Email,
                    Password = user.Account.Password.Value,
                    PasswordConfirmation = user.Account.Password.Value
                }
            };

            model.Name = "New name";

            var response = await Client.PutAsJsonAsync("User", model);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

}

using System.Data;
using System.Net;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.ViewModels;
using Parallel.Universe.Blog.Tests.Shared;
using Parallel.Universe.Blog.Tests.Shared.Builders;

namespace Parallel.Universe.Blog.Tests.IntegrationTests
{
    public class AccountTests : Integration
    {
        protected UserViewModelBuilder UserViewModelBuilder;

        [SetUp]
        public new void SetUp() => UserViewModelBuilder = new UserViewModelBuilder();
    }

    public class CreateTests : AccountTests
    {
        [Test]
        public async Task ShouldReturnOk()
        {
            var model = UserViewModelBuilder.WithAccount().Generate();
            var response = await Client.PostAsJsonAsync("Account/Create", model);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
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

            await new UserRepository(Context).AddAsync(user);
            await Context.SaveChangesAsync();

            var model = new LoginViewModel
            {
                Email = user.Account.Email,
                Senha = password
            };

            var response = await Client.PostAsJsonAsync("Account/Login", model);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

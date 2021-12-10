using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Services;
using Parallel.Universe.Blog.Tests.Shared.Builders.Models;
using System.IO;

namespace Parallel.Universe.Blog.Tests.Unit_Tests.Services
{
    public class TokenServiceTest
    {
        protected IConfiguration configuration;
        protected TokenService tokenService;
        protected UserBuilder userBuilder;

        [OneTimeSetUp]
        public void SetUp()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            tokenService = new TokenService(configuration);
            userBuilder = new UserBuilder();
        }
    }

    public class GenerateTokenTest : TokenServiceTest
    {

        private string tokenGenerated;

        [OneTimeSetUp]
        public new void SetUp()
        {
            var user = userBuilder.Generate();
            tokenGenerated = tokenService.GenerateToken(user);
        }


        [Test]
        public void ShouldNotBeEmpty() => tokenGenerated.Should().NotBeEmpty();
    }
}

﻿using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Data;
using System.Net.Http;
using Xunit;

namespace Parallel.Universe.Blog.Tests.Shared
{
    public class TestFixture : IClassFixture<BaseIntegrationTest>
    {
        protected HttpClient Client;
        protected ParallelUniverseBlogContext Context;

        [OneTimeSetUp]
        public void SetUp()
        {
            var factory = new BaseIntegrationTest();
            Context = factory.Services.CreateScope().ServiceProvider.GetService<ParallelUniverseBlogContext>();
            Client = factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services => services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>())).CreateClient();
        }
    }
}

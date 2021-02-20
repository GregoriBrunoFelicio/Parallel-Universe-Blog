using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoBogus;
using FluentAssertions;
using NUnit.Framework;
using Universo.Paralello.Blog.Api.ViewModels;
using Universo.Paralello.Blog.Tests.Shared;
using Universo.Paralello.Blog.Tests.Shared.Builders;

namespace Universo.Paralello.Blog.Tests.IntegrationTests
{

    public class ContaTests : Integration
    {
        protected UsuarioViewModelBuilder UsuarioViewModelBuilder;

        [SetUp]
        public new void SetUp() => UsuarioViewModelBuilder = new UsuarioViewModelBuilder();
    }

    public class CriarTests : ContaTests
    {

        [Test]
        public async Task DeveRetornarOk()
        {
            var model = UsuarioViewModelBuilder.WithConta().Generate();

            var response = await Client.PostAsJsonAsync("Conta/Criar", model);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

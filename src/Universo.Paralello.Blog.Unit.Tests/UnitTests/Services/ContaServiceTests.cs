using System.Threading.Tasks;
using AutoBogus;
using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Universo.Paralello.Blog.Api.Data.Repositories;
using Universo.Paralello.Blog.Api.Entities;
using Universo.Paralello.Blog.Api.Services;
using Universo.Paralello.Blog.Api.Services.Results;
using Universo.Paralello.Blog.Api.ViewModels;
using Universo.Paralello.Blog.Tests.Shared.Builders;

namespace Universo.Paralello.Blog.Tests.UnitTests.Services
{
    public class ContaServiceTests
    {
        protected Mock<IUsuarioRepository> UsuarioRepositoryMock;
        protected Mock<IContaRepository> ContaRepositoryMock;
        protected Mock<ITokenService> TokenServiceMock;
        protected Mock<IMapper> MapperMock;
        protected IContaService ContaService;
        protected UsuarioBuilder UsuarioBuilder;

        [OneTimeSetUp]
        public void SetUp()
        {
            UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            ContaRepositoryMock = new Mock<IContaRepository>();
            TokenServiceMock = new Mock<ITokenService>();
            MapperMock = new Mock<IMapper>();

            UsuarioBuilder = new UsuarioBuilder();

            ContaService = new ContaService(
                UsuarioRepositoryMock.Object,
                ContaRepositoryMock.Object,
                TokenServiceMock.Object,
                MapperMock.Object);
        }
    }

    public class CadastrarTests : ContaServiceTests
    {
        private IResult _resultado;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var model = new AutoFaker<CriacaoDeUsuarioViewModel>().Generate();
            var usuario = UsuarioBuilder.Generate();

            MapperMock.Setup(x => x.Map<CriacaoDeUsuarioViewModel, Usuario>(model)).Returns(usuario);
            _resultado = await ContaService.Cadastrar(model);
        }

        [Test]
        public void DeveChamarMetodoGetByEmailAsync() => ContaRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once);

        [Test]
        public void DeveChamarOMetodoDeMap() =>
            MapperMock.Verify(x => x.Map<CriacaoDeUsuarioViewModel, Usuario>(It.IsAny<CriacaoDeUsuarioViewModel>()), Times.Once);

        [Test]
        public void DeveChamarMetodoAddAsync() => UsuarioRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Usuario>()), Times.Once);

        [Test]
        public void OResultadoDeveRetonarAMensagemCorreta() =>
            _resultado.Mensagem.Should().Be("Usuário criado com sucesso.");

        [Test]
        public void OResultadoDeveRetornarSucessoVerdadeiro() => _resultado.Sucesso.Should().BeTrue();
    }
}

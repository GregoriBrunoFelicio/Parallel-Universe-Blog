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
        protected ContaBuilder ContaBuilder;
        protected UsuarioViewModelBuilder UsuarioViewModelBuilder;
        protected ContaViewModelBuilder ContaViewModelBuilder;

        [OneTimeSetUp]
        public void SetUp()
        {
            UsuarioRepositoryMock = new Mock<IUsuarioRepository>();
            ContaRepositoryMock = new Mock<IContaRepository>();
            TokenServiceMock = new Mock<ITokenService>();
            MapperMock = new Mock<IMapper>();

            UsuarioBuilder = new UsuarioBuilder();
            ContaBuilder = new ContaBuilder();
            UsuarioViewModelBuilder = new UsuarioViewModelBuilder();
            ContaViewModelBuilder = new ContaViewModelBuilder();

            ContaService = new ContaService(
                UsuarioRepositoryMock.Object,
                ContaRepositoryMock.Object,
                TokenServiceMock.Object,
                MapperMock.Object);
        }
    }

    public class CriarTests : ContaServiceTests
    {
        private IResult _resultado;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var model = UsuarioViewModelBuilder.WithConta().Generate();
            var usuario = UsuarioBuilder.Generate();

            MapperMock.Setup(x => x.Map<UsuarioViewModel, Usuario>(model)).Returns(usuario);
            _resultado = await ContaService.Criar(model);
        }

        [Test]
        public void DeveChamarMetodoGetByEmailAsync() => ContaRepositoryMock.Verify(x => x.GetByEmailAsync(It.IsAny<string>()), Times.Once);

        [Test]
        public void DeveChamarOMetodoDeMap() =>
            MapperMock.Verify(x => x.Map<UsuarioViewModel, Usuario>(It.IsAny<UsuarioViewModel>()), Times.Once);

        [Test]
        public void DeveChamarMetodoAddAsync() => UsuarioRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Usuario>()), Times.Once);

        [Test]
        public void OResultadoDeveRetonarAMensagemCorreta() =>
            _resultado.Mensagem.Should().Be("Usuário criado com sucesso.");

        [Test]
        public void OResultadoDeveRetornarSucessoVerdadeiro() => _resultado.Sucesso.Should().BeTrue();
    }

    public class CriarComEmailJaCadastradoTests : ContaServiceTests
    {
        private IResult _resultado;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var model = UsuarioViewModelBuilder.WithConta().Generate();
            ContaRepositoryMock.Setup(x => x.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync(new Conta());
            _resultado = await ContaService.Criar(model);
        }

        [Test]
        public void DeveRetonarMensagemCorretaQuandoOEmailJaEstaEmUso() => _resultado.Mensagem.Should().Be("Email já cadastrado.");

        [Test]
        public void OResultadoDeveRetornarSucessoComoFalso() => _resultado.Sucesso.Should().BeFalse();
    }

    public class AutenticarTests : ContaServiceTests
    {
        private IUsuarioLoginResult _resultado;
        private LoginViewModel _model;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var conta = ContaBuilder.Generate();
            _model = new LoginViewModel();

            _model.Senha = conta.Senha.Valor;
            conta.Senha.Criptografar();

            ContaRepositoryMock.Setup(x => x.GetByEmailAsync(_model.Email)).ReturnsAsync(conta);
            TokenServiceMock.Setup(x => x.GerarToken(It.IsAny<Usuario>())).Returns("token");
            _resultado = await ContaService.Autenticar(_model);
        }

        [Test]
        public void DeveChamarMetodoGetByEmail() =>
            ContaRepositoryMock.Verify(x => x.GetByEmailAsync(_model.Email), Times.Once);

        [Test]
        public void DeveChamarMetodoGetById() =>
            UsuarioRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);

        [Test]
        public void DeveChamarMetodoGerarToken() =>
                   TokenServiceMock.Verify(x => x.GerarToken(It.IsAny<Usuario>()), Times.Once);

        [Test]
        public void DeveRetornarMensagemDeSucesso() => _resultado.Mensagem.Should().Be("Login realizado com sucesso.");

        [Test]
        public void OResultadoDeveRetonarVerdadeiro() => _resultado.Sucesso.Should().BeTrue();

        [Test]
        public void DeveRetornarUmToken() => _resultado.Token.Should().NotBeEmpty();
    }

    public class AutenticarQuandoContaNaoExisteTests : ContaServiceTests
    {
        private IUsuarioLoginResult _resultado;
        private LoginViewModel _model;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            _model = new AutoFaker<LoginViewModel>().Generate();
            ContaRepositoryMock.Setup(x => x.GetByEmailAsync(_model.Email)).ReturnsAsync(value: null);
            _resultado = await ContaService.Autenticar(_model);
        }

        [Test]
        public void DeveChamarMetodoGetByEmail() =>
                ContaRepositoryMock.Verify(x => x.GetByEmailAsync(_model.Email), Times.Once);

        [Test]
        public void DeveRetornarMensagemDeEmailOuSenhaInvalidos() => _resultado.Mensagem.Should().Be("Email ou senha inválidos.");
    }

    public class AutenticarQuandoSenhaNaoEValidaTests : ContaServiceTests
    {
        private IUsuarioLoginResult _resultado;
        private LoginViewModel _model;

        [OneTimeSetUp]
        public new async Task SetUp()
        {
            var conta = ContaBuilder.Generate();
            _model = new AutoFaker<LoginViewModel>().Generate();

            conta.Senha.Criptografar();

            ContaRepositoryMock.Setup(x => x.GetByEmailAsync(_model.Email)).ReturnsAsync(conta);
            _resultado = await ContaService.Autenticar(_model);
        }

        [Test]
        public void DeveChamarMetodoGetByEmail() =>
            ContaRepositoryMock.Verify(x => x.GetByEmailAsync(_model.Email), Times.Once);

        [Test]
        public void DeveRetornarMensagemDeEmailOuSenhaInvalidos() => _resultado.Mensagem.Should().Be("Email ou senha inválidos.");
    }
}

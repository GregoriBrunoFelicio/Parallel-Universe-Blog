using System.Threading.Tasks;
using AutoMapper;
using Universo.Paralello.Blog.Api.Data.Repositories;
using Universo.Paralello.Blog.Api.Entities;
using Universo.Paralello.Blog.Api.Services.Results;
using Universo.Paralello.Blog.Api.ViewModels;

namespace Universo.Paralello.Blog.Api.Services
{
    public interface IContaService
    {
        Task<IResult> Criar(UsuarioViewModel model);
        Task<IUsuarioLoginResult> Autenticar(LoginViewModel model);
    }

    public class ContaService : IContaService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IContaRepository _contaRepository;
        private readonly ITokenService _tokeService;
        private readonly IMapper _mapper;

        public ContaService(IUsuarioRepository usuarioRepository, IContaRepository contaRepository, ITokenService tokeService, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _contaRepository = contaRepository;
            _tokeService = tokeService;
            _mapper = mapper;
        }

        public async Task<IResult> Criar(UsuarioViewModel model)
        {
            if (await EmailJaCadastrado(model.Conta.Email)) return new Result("Email já cadastrado.", false);

            var usuario = _mapper.Map<UsuarioViewModel, Usuario>(model);

            usuario.Conta.Senha.Criptografar();

            await _usuarioRepository.AddAsync(usuario);

            return new Result("Usuário criado com sucesso.", true);
        }

        public async Task<IUsuarioLoginResult> Autenticar(LoginViewModel model)
        {
            var conta = await _contaRepository.GetByEmailAsync(model.Email);

            if (conta == null)
                return new UsuarioLoginResult("Email ou senha inválidos.", false);

            if (!conta.Senha.Verificar(model.Senha))
                return new UsuarioLoginResult("Email ou senha inválidos.", false);

            var usuario = await _usuarioRepository.GetByIdAsync(conta.UsuarioId);
            var token = _tokeService.GerarToken(usuario);

            return new UsuarioLoginResult("Login realizado com sucesso.", true, token);
        }

        private async Task<bool> EmailJaCadastrado(string email) =>
            (await _contaRepository.GetByEmailAsync(email)) is not null;
    }
}

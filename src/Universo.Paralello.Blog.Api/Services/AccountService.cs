using System.Threading.Tasks;
using AutoMapper;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Services.Results;
using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Api.Services
{
    public interface IAccountService
    {
        Task<IResult> Create(UserViewModel model);
        Task<IUserLoginResult> Verify(LoginViewModel model);
    }

    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountService(IUserRepository userRepository, IAccountRepository accountRepository, ITokenService tokenService, IMapper mapper)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<IResult> Create(UserViewModel model)
        {
            if (await EmailAlreadyRegistred(model.Account.Email)) return new Result("E-mail already registred.", false);

            var user = _mapper.Map<UserViewModel, User>(model);

            user.Account.Password.Encrypt();

            await _userRepository.AddAsync(user);

            return new Result("User created successfully.", true);
        }

        public async Task<IUserLoginResult> Verify(LoginViewModel model)
        {
            var account = await _accountRepository.GetByEmailAsync(model.Email);

            if (account == null)
                return new UserLoginResult("Invalid email or password.", false);

            if (!account.Password.VerifyPassword(model.Senha))
                return new UserLoginResult("Invalid email or password.", false);

            var user = await _userRepository.GetByIdAsync(account.UserId);
            var token = _tokenService.GenerateToken(user);

            return new UserLoginResult("Login successfully.", true, token);
        }

        private async Task<bool> EmailAlreadyRegistred(string email) =>
            (await _accountRepository.GetByEmailAsync(email)) is not null;
    }
}

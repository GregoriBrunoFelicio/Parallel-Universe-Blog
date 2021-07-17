using AutoMapper;
using Parallel.Universe.Blog.Api.Data;
using Parallel.Universe.Blog.Api.Data.Repositories;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Services.Results;
using Parallel.Universe.Blog.Api.ViewModels;
using System;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Services
{
    public interface IAccountService
    {
        Task<IResult> Create(UserViewModel model);
        Task<ILoginResult> Verify(LoginInputModel model);
    }

    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUserRepository userRepository, IAccountRepository accountRepository, ITokenService tokenService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Create(UserViewModel model)
        {
            try
            {
                if (await EmailIsAlreadyRegistred(model.Account.Email)) return new Result("E-mail already registred.", false);

                var user = _mapper.Map<UserViewModel, User>(model);

                user.Account.Password.Encrypt();

                await _userRepository.AddAsync(user);

                return await _unitOfWork.CommitAsync()
                                 ? new Result("User created successfully.", true)
                                 : new Result("Error to create user.", false);
            }
            catch (Exception exception)
            {
                await _unitOfWork.RollBackAsync();
                return new Result(exception.Message, false);
            }
        }

        public async Task<ILoginResult> Verify(LoginInputModel model)
        {
            var account = await _accountRepository.GetByEmailAsync(model.Email);

            if (account == null)
                return new LoginResult("Invalid email or password.", false);

            if (!account.Password.VerifyPassword(model.Password))
                return new LoginResult("Invalid email or password.", false);

            if (!account.User.Active)
                return new LoginResult("Invalid email or password.", false);

            var token = _tokenService.GenerateToken(account.User);

            var user = new UserInfoViewModel(account.UserId, account.User.Name, account.User.About);

            return new LoginResult("Login successfully.", true, user, token);
        }

        private async Task<bool> EmailIsAlreadyRegistred(string email) =>
            (await _accountRepository.GetByEmailAsync(email)) is not null;
    }
}

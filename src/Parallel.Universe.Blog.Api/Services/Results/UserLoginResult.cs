using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Api.Services.Results
{
    public interface ILoginResult : IResult
    {
        string Token { get; }
    }

    public class LoginResult : ILoginResult
    {
        public LoginResult(string mensagem, bool sucesso, UserInfoViewModel user = default, string token = "")
        {
            Message = mensagem;
            Success = sucesso;
            Token = token;
            User = user;
        }

        public string Token { get; }
        public string Message { get; }
        public bool Success { get; }
        public UserInfoViewModel User { get; set; }
    }
}

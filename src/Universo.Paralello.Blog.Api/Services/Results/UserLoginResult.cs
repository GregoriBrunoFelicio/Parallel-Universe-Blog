namespace Parallel.Universe.Blog.Api.Services.Results
{
    public interface IUserLoginResult : IResult
    {
        string Token { get; }
    }

    public class UserLoginResult : IUserLoginResult
    {
        public UserLoginResult(string mensagem, bool sucesso, string token = "")
        {
            Message = mensagem;
            Success = sucesso;
            Token = token;
        }

        public string Token { get; }
        public string Message { get; }
        public bool Success { get; }
    }
}

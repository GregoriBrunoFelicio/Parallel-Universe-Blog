namespace Parallel.Universe.Blog.Api.Services.Results
{
    public interface ILoginResult : IResult
    {
        string Token { get; }
    }

    public class LoginResult : ILoginResult
    {
        public LoginResult(string mensagem, bool sucesso, string token = "")
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

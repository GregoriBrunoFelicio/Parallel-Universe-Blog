namespace Parallel.Universe.Blog.Api.Services.Results
{
    public interface ILoginResult : IResult
    {
        string Token { get; }
    }

    public class LoginResult : ILoginResult
    {
        public LoginResult(string mensagem, bool sucesso, int userId = default, string userName = "", string token = "")
        {
            Message = mensagem;
            Success = sucesso;
            UserId = userId;
            UserName = userName;
            Token = token;
        }

        public string Token { get; }
        public string Message { get; }
        public bool Success { get; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
}

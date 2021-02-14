namespace Universo.Paralello.Blog.Api.Services.Results
{
    public interface IUsuarioLoginResult : IResult
    {
        string Token { get; }
    }

    public class UsuarioLoginResult : IUsuarioLoginResult
    {
        public UsuarioLoginResult(string mensagem, bool sucesso, string token = "")
        {
            Mensagem = mensagem;
            Sucesso = sucesso;
            Token = token;
        }

        public string Token { get; }
        public string Mensagem { get; }
        public bool Sucesso { get; }
    }
}

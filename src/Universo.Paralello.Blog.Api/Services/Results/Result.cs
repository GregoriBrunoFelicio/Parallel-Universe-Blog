namespace Universo.Paralello.Blog.Api.Services.Results
{
    public interface IResult
    {
        string Mensagem { get; }
        bool Sucesso { get; }
    }

    public class Result : IResult
    {
        public Result(string mensagem, bool sucesso)
        {
            Mensagem = mensagem;
            Sucesso = sucesso;
        }

        public string Mensagem { get; }
        public bool Sucesso { get; }
    }
}

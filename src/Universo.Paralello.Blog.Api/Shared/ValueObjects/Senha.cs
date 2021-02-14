namespace Universo.Paralello.Blog.Api.Shared.ValueObjects
{
    public class Senha
    {
        public Senha(string valor) => Valor = valor;

        public string Valor { get; set; }

        public string Criptografar() =>  Valor =
              BCrypt.Net.BCrypt.HashPassword(Valor);

        public bool Verificar(string senha) =>
            BCrypt.Net.BCrypt.Verify(senha, Valor);
    }
}

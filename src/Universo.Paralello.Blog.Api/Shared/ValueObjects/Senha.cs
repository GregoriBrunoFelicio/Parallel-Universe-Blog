using static BCrypt.Net.BCrypt;

namespace Universo.Paralello.Blog.Api.Shared.ValueObjects
{
    public class Senha
    {
        public Senha(string valor) => Valor = valor;

        public string Valor { get; set; }

        public string Criptografar() =>  Valor =
              HashPassword(Valor);

        public bool Verificar(string senha) =>
            Verify(senha, Valor);
    }
}

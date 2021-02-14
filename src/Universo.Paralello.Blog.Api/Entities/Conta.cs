using Universo.Paralello.Blog.Api.Shared;
using Universo.Paralello.Blog.Api.Shared.ValueObjects;

namespace Universo.Paralello.Blog.Api.Entities
{
    public class Conta : Entidade
    {
        public Conta() { }

        public Conta(int id, string email, Senha senha)
        {
            Id = id;
            Email = email;
            Senha = senha;
        }

        public string Email { get; }
        public Senha Senha { get; }
        public int UsuarioId { get; }
        public virtual Usuario Usuario { get; }

    }
}

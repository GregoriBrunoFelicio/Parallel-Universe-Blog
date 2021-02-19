namespace Universo.Paralello.Blog.Api.Entities
{
    public class Usuario : Entity
    {
        public Usuario() {}

        public Usuario(int id, string nome, string sobre)
        {
             Id = id;
            Nome = nome;
            Sobre = sobre;
        }

        public Usuario(int id, string nome, Conta conta)
        {
            Id = id;
            Nome = nome;
            Conta = conta;
        }

        public string Nome { get; }
        public string Sobre { get; }
        public virtual Conta Conta { get; }
    }
}

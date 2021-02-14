using Universo.Paralello.Blog.Api.Entities;

namespace Universo.Paralello.Blog.Api.Data.Repositories
{
    public interface IUsuarioRepository: IRepository<Usuario>
    {

    }

    public class UsuarioRepository: Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(UniversoParalelloBlogContext context) : base(context)
        {
        }
    }
}

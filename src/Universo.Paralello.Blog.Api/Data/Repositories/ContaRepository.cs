using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Universo.Paralello.Blog.Api.Entities;

namespace Universo.Paralello.Blog.Api.Data.Repositories
{
    public interface IContaRepository : IRepository<Conta>
    {
        Task<Conta> GetByEmailAsync(string email);
    }

    public class ContaRepository: Repository<Conta>, IContaRepository
    {
        public ContaRepository(UniversoParalelloBlogContext context) : base(context)
        {
        }

        public async Task<Conta> GetByEmailAsync(string email) => await Context.Set<Conta>().SingleOrDefaultAsync(x => x.Email == email);
    }
}

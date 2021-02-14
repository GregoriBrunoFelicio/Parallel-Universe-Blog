using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Universo.Paralello.Blog.Api.Entities;

namespace Universo.Paralello.Blog.Api.Data.Repositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T obj);
        Task<T> GetByIdAsync(int id);
    }

    public class Repository<T> : IRepository<T> where T : Entidade
    {
        protected UniversoParalelloBlogContext Context;
        private readonly DbSet<T> _dbSet;

        public Repository(UniversoParalelloBlogContext context)
        {
            Context = context;
            _dbSet = Context.Set<T>();
        }

        public async Task AddAsync(T obj)
        {
            await Context.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id) => await _dbSet.SingleOrDefaultAsync(x => x.Id == id);
    }
}

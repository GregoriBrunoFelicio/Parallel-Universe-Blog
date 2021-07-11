using Microsoft.EntityFrameworkCore;
using Parallel.Universe.Blog.Api.Entities;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task<T> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }

    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected ParallelUniverseBlogContext Context;
        private readonly DbSet<T> _dbSet;

        public Repository(ParallelUniverseBlogContext context)
        {
            Context = context;
            _dbSet = Context.Set<T>();
        }

        public virtual async Task AddAsync(T obj) => await Context.AddAsync(obj);

        public virtual async Task UpdateAsync(T obj)
        {
            var objFromDb = await _dbSet.FindAsync(obj.Id);
            Context.Entry(objFromDb).CurrentValues.SetValues(obj);
        }

        public virtual async Task<T> GetByIdAsync(int id) => await _dbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

        public virtual async Task DeleteAsync(int id) => Context.Remove(await _dbSet.FindAsync(id));
    }
}

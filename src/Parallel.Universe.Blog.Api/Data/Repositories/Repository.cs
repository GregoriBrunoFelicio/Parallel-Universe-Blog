using Microsoft.EntityFrameworkCore;
using Parallel.Universe.Blog.Api.Entities;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T obj);
        Task<T> UpdateAsync(T obj);
        Task<T> GetByIdAsync(int id);
        Task<bool> Delete(int id);
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

        public async Task AddAsync(T obj)
        {
            await Context.AddAsync(obj);
            await Context.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T obj)
        {
            var objFromDb = await GetByIdAsync(obj.Id);
            Context.Entry(objFromDb).CurrentValues.SetValues(obj);
            await Context.SaveChangesAsync();
            return obj;
        }

        public async Task<T> GetByIdAsync(int id) => await _dbSet.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<bool> Delete(int id)
        {
            var obj = await GetByIdAsync(id);
            Context.Remove(obj);
            return await Context.SaveChangesAsync() > 0;
        }
    }
}

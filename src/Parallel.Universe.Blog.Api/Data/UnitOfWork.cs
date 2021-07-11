using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        Task RoolBack();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ParallelUniverseBlogContext context;

        public UnitOfWork(ParallelUniverseBlogContext context) => this.context = context;

        public async Task<bool> CommitAsync() => await context.SaveChangesAsync() > 0;

        public async Task RoolBack() => await context.DisposeAsync();
    }
}
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        Task RollBackAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ParallelUniverseBlogContext context;
        private readonly IDbContextTransaction transaction;

        public UnitOfWork(ParallelUniverseBlogContext context)
        {
            this.context = context;
            this.transaction = context.Database.BeginTransaction();
        }

        public async Task<bool> CommitAsync() => await context.SaveChangesAsync() > 0;

        public async Task RollBackAsync() => await transaction.RollbackAsync();
    }
}
using Microsoft.EntityFrameworkCore;
using Parallel.Universe.Blog.Api.Entities;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByEmailAsync(string email);
    }

    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(ParallelUniverseBlogContext context) : base(context)
        {
        }

        public async Task<Account> GetByEmailAsync(string email) =>
                await Context.Set<Account>()
                    .Include(x => x.User)
                    .AsNoTracking()
                    .SingleOrDefaultAsync(x => x.Email == email);
    }
}

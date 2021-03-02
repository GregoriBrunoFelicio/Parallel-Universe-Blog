using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByEmailAsync(string email);
    }

    public class AccountRepository: Repository<Account>, IAccountRepository
    {
        public AccountRepository(ParallelUniverseBlogContext context) : base(context)
        {
        }

        public async Task<Account> GetByEmailAsync(string email) => 
                await Context.Set<Account>()
                    .SingleOrDefaultAsync(x => x.Email == email);
    }
}

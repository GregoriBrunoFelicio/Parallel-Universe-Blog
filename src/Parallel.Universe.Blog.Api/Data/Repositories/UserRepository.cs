using Parallel.Universe.Blog.Api.Entities;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task Inactive(int id);
    }

    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ParallelUniverseBlogContext context) : base(context)
        {
        }

        public async Task Inactive(int id)
        {
            var userFromDb = await GetByIdAsync(id);
            var user = new User(userFromDb.Id, userFromDb.Name, userFromDb.About, userFromDb.Account, false);
            await UpdateAsync(user);
        }
    }
}

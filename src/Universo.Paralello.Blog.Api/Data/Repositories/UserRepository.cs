using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface IUserRepository: IRepository<User>
    {

    }

    public class UserRepository: Repository<User>, IUserRepository
    {
        public UserRepository(ParallelUniverseBlogContext context) : base(context)
        {
        }
    }
}

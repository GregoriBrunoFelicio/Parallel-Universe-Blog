using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {

    }


    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ParallelUniverseBlogContext context) : base(context)
        {
        }
    }
}

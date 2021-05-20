using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {

    }

    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ParallelUniverseBlogContext context) : base(context)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Parallel.Universe.Blog.Api.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IReadOnlyCollection<Post>> GetAllByPostId(int id);
    }

    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ParallelUniverseBlogContext context) : base(context)
        {
        }

        public async Task<IReadOnlyCollection<Post>> GetAllByPostId(int id) =>
            await Context.Post.AsNoTracking()
                .Where(x => x.Id == id)
                .ToListAsync();
    }
}

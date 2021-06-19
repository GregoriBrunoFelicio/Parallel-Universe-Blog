using Microsoft.EntityFrameworkCore;
using Parallel.Universe.Blog.Api.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<IReadOnlyCollection<Post>> GetAllActivePostsAsync();
    }


    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ParallelUniverseBlogContext context) : base(context)
        {
        }

        public async Task<IReadOnlyCollection<Post>> GetAllActivePostsAsync() =>
            await Context.Post
                .AsNoTracking()
                .Where(x => x.Active)
                .ToListAsync();
    }

}

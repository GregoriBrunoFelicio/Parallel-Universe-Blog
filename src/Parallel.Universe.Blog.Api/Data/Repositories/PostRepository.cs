using Microsoft.EntityFrameworkCore;
using Parallel.Universe.Blog.Api.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parallel.Universe.Blog.Api.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        public Task<IReadOnlyCollection<Post>> GetAllActiveAsync();
        public Task Inactive(int id);
    }

    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ParallelUniverseBlogContext context) : base(context)
        {
        }

        public async Task<IReadOnlyCollection<Post>> GetAllActiveAsync() =>
            await Context.Post
                .AsNoTracking()
                .Where(x => x.Active)
                .OrderBy(x => x.Date)
                .ToListAsync();

        public async Task Inactive(int id)
        {
            var postFromdb = await GetByIdAsync(id);
            var post = new Post(postFromdb.Id, postFromdb.Title, postFromdb.Description, postFromdb.Text, postFromdb.Date, false, postFromdb.UserId);
            await UpdateAsync(post);
        }
    }

}

using Microsoft.EntityFrameworkCore;
using Parallel.Universe.Blog.Api.Data.Mappings;

namespace Parallel.Universe.Blog.Api.Data
{
    public class ParallelUniverseBlogContext : DbContext
    {
        public ParallelUniverseBlogContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => 
            modelBuilder
                .ApplyConfiguration(new UserMapping())
                .ApplyConfiguration(new AccountMapping());
    }
}

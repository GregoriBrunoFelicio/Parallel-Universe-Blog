using Microsoft.EntityFrameworkCore;
using Universo.Paralello.Blog.Api.Data.Mappings;

namespace Universo.Paralello.Blog.Api.Data
{
    public class UniversoParalelloBlogContext : DbContext
    {
        public UniversoParalelloBlogContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => 
            modelBuilder
                .ApplyConfiguration(new UsuarioMapping())
                .ApplyConfiguration(new ContaMapping());
    }
}

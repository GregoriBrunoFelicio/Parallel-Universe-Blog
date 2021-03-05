using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Api.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("varchar").IsRequired().HasMaxLength(50);
            builder.Property(x => x.About).HasColumnType("varchar").HasMaxLength(500);
            builder.HasOne(x => x.Account).WithOne(x => x.User).HasForeignKey<Account>(x => x.UserId).IsRequired();
            builder.Property(x => x.Active).HasColumnType("bit").IsRequired();
        }
    }

    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).HasColumnType("varchar").IsRequired().HasMaxLength(30);
            builder.OwnsOne(x => x.Password).Property(x => x.Value).HasColumnName("Password").HasColumnType("varchar(MAX)").IsRequired();
        }
    }

    public class PostMapping : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasColumnType("varchar").HasMaxLength(500);
            builder.Property(x => x.Text).IsRequired().HasColumnType("varchar(MAX)");
            builder.Property(x => x.Date).IsRequired().HasColumnType("date");
            builder.Property(x => x.Active).IsRequired().HasColumnType("bit");
            builder.HasOne(x => x.User).WithMany(x => x.Posts).IsRequired();
        }
    }
}

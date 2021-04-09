﻿using Microsoft.EntityFrameworkCore;
using Parallel.Universe.Blog.Api.Data.Mappings;
using Parallel.Universe.Blog.Api.Entities;
using System.Security.Claims;

namespace Parallel.Universe.Blog.Api.Data
{
    public class ParallelUniverseBlogContext : DbContext
    {
        public virtual ClaimsPrincipal User { get; set; }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Post> Post { get; set; }

        public ParallelUniverseBlogContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder
                .ApplyConfiguration(new UserMapping())
                .ApplyConfiguration(new AccountMapping())
                .ApplyConfiguration(new PostMapping());
    }
}

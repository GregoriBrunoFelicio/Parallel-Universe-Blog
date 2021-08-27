using Bogus;
using Parallel.Universe.Blog.Api.Entities;

namespace Parallel.Universe.Blog.Tests.Shared.Builders.Models
{
    public sealed class PostBuilder : Faker<Post>
    {
        public PostBuilder() =>
            CustomInstantiator(f => new Post(
                0,
                f.Random.Words(),
                f.Random.Words(),
                f.Random.Words(),
                f.Date.Future(),
                f.Random.Bool(),
                0
            ));


        public PostBuilder WithUserId(int userId)
        {
            CustomInstantiator(f => new Post(
                          0,
                          f.Random.Words(),
                          f.Random.Words(),
                          f.Random.Words(),
                          f.Date.Future(),
                          f.Random.Bool(),
                          userId
                      ));
            return this;
        }
    }
}

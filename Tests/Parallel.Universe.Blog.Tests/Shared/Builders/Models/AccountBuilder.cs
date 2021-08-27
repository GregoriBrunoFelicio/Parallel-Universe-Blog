using Bogus;
using Parallel.Universe.Blog.Api.Entities;
using Parallel.Universe.Blog.Api.Shared.ValueObjects;

namespace Parallel.Universe.Blog.Tests.Shared.Builders.Models
{
    public sealed class AccountBuilder : Faker<Account>
    {
        public AccountBuilder() =>
            CustomInstantiator(f =>
                new Account(0,
                    f.Internet.Email(), new Password(f.Random.Words())));
    }
}

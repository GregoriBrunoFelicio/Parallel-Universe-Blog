﻿using Bogus;
using Parallel.Universe.Blog.Api.ViewModels;

namespace Parallel.Universe.Blog.Tests.Shared.Builders
{
    public sealed class AccountInputModelBuilder : Faker<AccountInputModel>
    {
        public AccountInputModelBuilder() =>
            RuleFor(f => f.Id, () => 0)
                .RuleFor(f => f.Email, f => f.Internet.Email())
                .RuleFor(f => f.Password, f => f.Internet.Password())
                .RuleFor(f => f.PasswordConfirmation, f => f.Internet.Password());
    }
}

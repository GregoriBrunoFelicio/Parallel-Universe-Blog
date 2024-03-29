﻿using Bogus;
using FluentAssertions;
using NUnit.Framework;
using Parallel.Universe.Blog.Api.Shared.ValueObjects;

namespace Parallel.Universe.Blog.Tests.Unit_Tests.ValueObjects
{
    public class PasswordTests
    {
        private readonly Faker _faker = new();

        [Test]
        public void ShouldEncryptTheReceivedPassword()
        {
            var value = _faker.Random.Word();
            var password = new Password(value);
            password.Encrypt();
            value.Should().NotBe(password.Value);
        }

        [Test]
        public void ShouldReturnTrueWhenThePasswordIsNotCorrect()
        {
            var value = _faker.Random.Word();
            var password = new Password(value);
            password.Encrypt();
            password.VerifyPassword(value).Should().BeTrue();
        }

        [Test]
        public void ShouldReturnFalseWhenThePasswordIsNotCorrect()
        {
            var value = _faker.Random.Word();
            var password = new Password(value);
            password.Encrypt();
            password.VerifyPassword($"{value}123").Should().BeFalse();
        }
    }
}

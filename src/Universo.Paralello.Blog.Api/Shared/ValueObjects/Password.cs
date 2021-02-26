using static BCrypt.Net.BCrypt;

namespace Parallel.Universe.Blog.Api.Shared.ValueObjects
{
    public class Password
    {
        public Password(string value) => Value = value;

        public string Value { get; set; }

        public string Encrypt() =>  Value =
              HashPassword(Value);

        public bool VerifyPassword(string password) =>
            Verify(password, Value);
    }
}

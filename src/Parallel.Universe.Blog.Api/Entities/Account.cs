using Parallel.Universe.Blog.Api.Shared.ValueObjects;

namespace Parallel.Universe.Blog.Api.Entities
{
    public class Account : Entity
    {
        protected Account() { }

        public Account(int id, string email, Password password) : base(id)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public Password Password { get; }
        public int UserId { get; }
        public virtual User User { get; protected set; }

        public void SetUser(User user) => User = user;
    }
}

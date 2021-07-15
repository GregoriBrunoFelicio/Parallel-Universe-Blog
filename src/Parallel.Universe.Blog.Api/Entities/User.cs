using System.Collections.Generic;

namespace Parallel.Universe.Blog.Api.Entities
{
    public class User : Entity
    {
        protected User() { }

        public User(int id, string name, string about) : base(id)
        {
            Name = name;
            About = about;
        }

        public User(int id, string name, string about, Account account, bool active) : base(id)
        {
            Name = name;
            About = about;
            Account = account;
            Active = active;
        }

        public string Name { get; }
        public string About { get; }
        public bool Active { get; }
        public virtual Account Account { get; }
        public virtual ICollection<Post> Posts { get; }
    }
}

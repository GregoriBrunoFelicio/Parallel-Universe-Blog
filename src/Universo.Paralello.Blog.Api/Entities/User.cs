using System.Collections.Generic;

namespace Parallel.Universe.Blog.Api.Entities
{
    public class User : Entity
    {
        public User() { }

        public User(int id, string name, string about)
        {
            Id = id;
            Name = name;
            About = about;
        }

        public User(int id, string name, Account account, bool active)
        {
            Id = id;
            Name = name;
            Account = account;
            Active = active;
        }

        public string Name { get; }
        public string About { get; }
        public bool Active { get; set; }
        public virtual Account Account { get; }
        public virtual ICollection<Post> Posts { get; }
    }
}

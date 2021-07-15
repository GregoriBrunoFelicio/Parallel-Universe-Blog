using System;

namespace Parallel.Universe.Blog.Api.Entities
{
    public class Comment : Entity
    {
        protected Comment()
        {
        }

        public Comment(int id, string content, DateTime date, int postId, bool active) : base(id)
        {
            Content = content;
            Date = date;
            PostId = postId;
            Active = active;
        }

        public string Content { get; }
        public DateTime Date { get; }
        public int PostId { get; }
        public bool Active { get; }
        public virtual Post Post { get; }

    }
}

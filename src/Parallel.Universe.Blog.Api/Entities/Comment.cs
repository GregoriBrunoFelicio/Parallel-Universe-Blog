using System;

namespace Parallel.Universe.Blog.Api.Entities
{
    public class Comment : Entity
    {
        public Comment()
        {

        }

        public Comment(int id, string content, DateTime date, int postId) : base(id)
        {
            Content = content;
            Date = date;
            PostId = postId;
        }

        public string Content { get; }
        public DateTime Date { get; }
        public int PostId { get; }
        public virtual Post Post { get; }
    }
}

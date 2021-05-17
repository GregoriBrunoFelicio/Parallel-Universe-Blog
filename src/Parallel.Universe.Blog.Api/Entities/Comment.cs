using System;

namespace Parallel.Universe.Blog.Api.Entities
{
    public class Comment : Entity
    {
        public Comment(int id, string content, DateTime date) : base(id)
        {
            Content = content;
            Date = date;
        }

        public string Content { get; }
        public DateTime Date { get; }
    }
}

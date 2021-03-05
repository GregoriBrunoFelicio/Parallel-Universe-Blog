using System;

namespace Parallel.Universe.Blog.Api.Entities
{
    public class Post : Entity
    {
        public Post(int id, string title, string description, string text, int userId)
        {
            Id = id;
            Title = title;
            Description = description;
            Text = text;
            UserId = userId;
        }

        public Post(int id, string title, string description, string text, DateTime date, bool active, int userId)
        {
            Id = id;
            Title = title;
            Description = description;
            Text = text;
            Date = date;
            Active = active;
            UserId = userId;
        }

        public string Title { get; }
        public string Description { get; }
        public string Text { get; }
        public DateTime Date { get; }
        public bool Active { get; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

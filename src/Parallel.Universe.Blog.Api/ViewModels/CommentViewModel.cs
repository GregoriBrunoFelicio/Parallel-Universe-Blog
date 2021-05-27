using System;
using System.ComponentModel.DataAnnotations;

namespace Parallel.Universe.Blog.Api.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The content is required.")]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int PostId { get; set; }
    }
}

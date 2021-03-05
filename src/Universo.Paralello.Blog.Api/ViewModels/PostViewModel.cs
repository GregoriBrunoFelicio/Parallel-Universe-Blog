using System;
using System.ComponentModel.DataAnnotations;

namespace Parallel.Universe.Blog.Api.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "The Title is required.")]
        [StringLength(50, ErrorMessage = "The Title must be between {2} and {1} characters.", MinimumLength = 5)]

        public string Title { get; set; }


        [Required(ErrorMessage = "The Description is required.")]

        [StringLength(500, ErrorMessage = "The Description must be between {2} and {1} characters.", MinimumLength = 5)]
        public string Description { get; set; }


        [Required(ErrorMessage = "The Description is required.")]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }

    }
}

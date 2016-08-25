using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebStepBlog.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public ApplicationUser Author { get; set; }

        public Comment Comment { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace blogApi.Models
{
    public class Comment
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string commentBody { get; set; }
        public int? userId { get; set; }
        public User user { get; set; }
        [Required]
        public int postId { get; set; }
        public Post post { get; set; }
       
        

    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace blogApi.Models
{
    public class User
    {


        [Key]
        public int userId { get; set; }
        [Required]
        public string userName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string userRole {get; set;}
        [Required]
        public string password {get; set;}
        public List<Post> Posts { get; set; }
        

    }
}
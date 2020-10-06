using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;



namespace blogApi.Models
{
    public class Post
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string body { get; set; }
        [Required]
        public int userId {get; set;}
        [JsonIgnore]
        public User user { get; set; }
        [Required]
        public string status {get; set;}
        

    }
}
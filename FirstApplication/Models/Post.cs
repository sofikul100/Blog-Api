using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApplication.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = "";        
        public string Content { get; set; }= "";
        public DateTime CreatedAt { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        // Navigation Properties
        public User User { get; set; } =default!;
        public Category Category {get;set;} =default!;
        public ICollection<Comment> Comments { get; set; }=default!;
    }
}
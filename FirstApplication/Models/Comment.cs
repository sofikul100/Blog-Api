using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApplication.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }

        // Foreign Keys
        public int? UserId { get; set; }
        public int PostId { get; set; }

        // Navigation Properties
        public User User { get; set; } =default!;
        public Post Post { get; set; } =default!;

        
    }


}
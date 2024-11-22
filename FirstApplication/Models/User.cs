using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApplication.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }="";

        [Required]
        [EmailAddress]
        public string Email { get; set; }="";

        // Navigation Properties
        public ICollection<Post> Posts { get; set; }=default!;
        public ICollection<Comment> Comments { get; set; }=default!;
    }
}
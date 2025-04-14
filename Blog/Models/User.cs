using Microsoft.Extensions.Hosting;

using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; } = "";

        //[Required]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; } = "";

        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; } = "";

        public ICollection<BlogPost> Posts { get; set; } = new List<BlogPost>();
    }
}



using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiDemo.Models
{
    public class BlogEntity : DbContext
    {
        public BlogEntity()
        {
        }

        public BlogEntity(DbContextOptions<BlogEntity> options) : base(options)
        {
        }

        public DbSet<BlogPost > BlogPosts  { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=BlogDB; Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

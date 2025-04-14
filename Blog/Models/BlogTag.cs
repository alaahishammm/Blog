using System.Security.Principal;

namespace Blog.Models
{
    public class BlogTag
    {
        public int  id  { get; set; }
        public string TagName { get; set; }
        public  List<BlogPost> BlogPosts  { get; set; }
    }
}

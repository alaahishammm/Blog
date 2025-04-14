namespace Blog.Models
{
    public class BlogPost
    {
        public int  id { get; set; }
        public string  heading  { get; set; }
        public  string Pagetitle  { get; set; }
        public  string   content { get; set; }
        public  string Auther { get; set; }
        public DateTime  PublishDate { get; set; }
        public bool visable { get; set; }
        public string? ImageUrl { get; set; }
        public List<BlogTag > BlogTags { get; set; }
        public User? User { get; set; } // Navigation property
    
}
}

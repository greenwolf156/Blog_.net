using System.Data.Entity;

namespace BlogsConsole.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext() : base("name=BlogContext") { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public void AddBlog(Blog blog)
        {
            this.Blogs.Add(blog);
            this.SaveChanges();
        }
        public void FindBlog(string blog)
        {
            this.Blogs.Find(blog);
        }
        public Blog RealBlog(string blog)
        {
            return this.Blogs.Find(blog);
            
        }
        public Blog RealBlog(int blogid)
        {
            return this.Blogs.Find(blogid);

        }
        public void MakePost(Post p)
        {
            this.Posts.Add(p);
            this.SaveChanges();

        }

    }
}

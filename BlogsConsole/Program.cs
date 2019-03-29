using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using BlogsConsole.Models;

namespace BlogsConsole
{
    class MainClass
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            logger.Info("Program started");
            try
            {
                string choice;
                string bln;
                Console.WriteLine("1) Display all blogs");
                Console.WriteLine("2) Add blog");
                Console.WriteLine("3) Create Post");
                Console.WriteLine("Enter to quit");
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);
                var db = new BloggingContext();
                if (choice == "1")
                {
                    var query = db.Blogs.OrderBy(b => b.Name);

                    Console.WriteLine("All blogs in the database:");
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
                else if (choice == "2")
                {
                    // Create and save a new Blog
                    Console.Write("Enter a name for a new Blog: ");
                    var name = Console.ReadLine();

                    var blog = new Blog { Name = name };

                    
                    db.AddBlog(blog);
                    logger.Info("Blog added - {name}", name);
                }
                else if (choice == "3")
                {
                    Console.Write("Enter blog name: ");
                    bln = Console.ReadLine();
                    Blog bl = db.RealBlog(bln);
                    Console.Write("Enter Post Title: ");
                    string blpt = Console.ReadLine();
                    Console.Write("Enter Post Content: ");
                    string blpc = Console.ReadLine();
                    var post = new Post { Title = blpt, Content = blpc, Blog = bl};
                    db.MakePost(post);
                    logger.Info("Post made");
                } while (choice == "1" || choice == "2"|| choice == "3") ;

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}

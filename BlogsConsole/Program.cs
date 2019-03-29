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
                string choice = "";
                do
                {
                    Console.WriteLine("Enter Selection: ");
                    Console.WriteLine("1) Display all blogs");
                    Console.WriteLine("2) Add blog");
                    Console.WriteLine("3) Create Post");
                    Console.WriteLine("Enter q to quit");
                    choice = Console.ReadLine();
                    logger.Info("User choice: {Choice}", choice);
                    var db = new BloggingContext();
                    if (choice == "1")
                    {
                        var query = db.Blogs.OrderBy(b => b.Name);
                        int c = db.Blogs.Count();

                        Console.WriteLine("{0} Blogs returned", c);
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
                        if (name == "")
                        {
                            logger.Error("Blog Name cannot be null");
                        }
                        else
                        {
                            var blog = new Blog { Name = name };


                            db.AddBlog(blog);
                            logger.Info("Blog added - {name}", name);
                        }
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Select the blog you want to post to: ");
                        var query = db.Blogs.OrderBy(b => b.Name);
                        int bln;
                        foreach (var item in query)
                        {
                            Console.WriteLine(item.BlogId + ") " + item.Name);
                        }

                        if (!int.TryParse(Console.ReadLine(), out bln))
                        {
                            logger.Error("Invalid Blog Id");
                        }
                        else
                        {
                            if (bln > 0 && bln <= db.Blogs.Count())
                            {
                                Blog bl = db.RealBlog(bln);
                                Console.Write("Enter Post Title: ");
                                string blpt = Console.ReadLine();
                                if(blpt == "")
                                {
                                    logger.Error("Post title cannot be null");
                                }
                                else
                                {
                                    Console.Write("Enter Post Content: ");
                                    string blpc = Console.ReadLine();
                                    var post = new Post { Title = blpt, Content = blpc, Blog = bl, BlogId = bln};
                                    db.MakePost(post);
                                    logger.Info("Post added - \"{blpt}\"", blpt);
                                }
                            }
                            else
                            {
                                logger.Error("There are no Blogs saved with that Id");
                            }
                        }

                        //Console.Write("Enter blog name: ");
                        //bln = Console.ReadLine();
                        //Blog bl = db.RealBlog(bln);
                        //Console.Write("Enter Post Title: ");
                        //string blpt = Console.ReadLine();
                        //Console.Write("Enter Post Content: ");
                        //string blpc = Console.ReadLine();
                        //var post = new Post { Title = blpt, Content = blpc, Blog = bl };
                        //db.MakePost(post);
                        //logger.Info("Post made");
                    }
                    Console.WriteLine();
                } while ((choice != "q" && choice != "Q") && (choice == "1" || choice == "2" || choice == "3"));
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}

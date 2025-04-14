using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using WebApiDemo.Models;
using System.IO;
using System;

namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private readonly BlogEntity _context;
        private readonly IWebHostEnvironment _env;

        public PostController(BlogEntity context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> OrderPosts()
        {
            var posts = await _context.BlogPosts
                .OrderByDescending(p => p.PublishDate)
                .ToListAsync();

            return View(posts);
        }
        //get
        public IActionResult CreatePost()
        {
            return View();
        }

        // post
       

        [HttpPost]
        public async Task<IActionResult> CreatePost(string content, IFormFile image, string Auther, string Pagetitle, string heading)
        {
            
            if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(Pagetitle))
            {
                ModelState.AddModelError("", "Content and Page title are required.");
                return View(); 
            }

            heading = string.IsNullOrWhiteSpace(heading) ? Pagetitle : heading;

            var post = new BlogPost
            {
                content = content,
                PublishDate = DateTime.UtcNow,
                Auther = Auther ?? "Anonymous", 
                Pagetitle = Pagetitle,
                heading = heading,
            };

            if (image != null && image.Length > 0)
            {
                var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadFolder); 

                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadFolder, fileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                    post.ImageUrl = "/uploads/" + fileName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Failed to upload image: {ex.Message}");
                    return View(); 
                }
            }

            try
            {
                _context.BlogPosts.Add(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Failed to save post: {ex.Message}");
                return View(); 
            }

            return RedirectToAction("OrderPosts");
        }
        // delete post 
       [HttpPost ]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.BlogPosts.FindAsync(id);

            if (post == null)
            {
                return NotFound(); // Return 404
            }

            try
            {
                _context.BlogPosts.Remove(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting post: {ex.Message}");
                return RedirectToAction("OrderPosts"); // error message
            }

            return RedirectToAction("OrderPosts");
        }
        // edit a post
        [HttpPost]
        public async Task<IActionResult> EditPost(int id, string content, IFormFile image, string Auther, string Pagetitle, string heading)
        {
            var post = await _context.BlogPosts.FindAsync(id);

            if (post == null)
            {
                return NotFound(); 
            }


            if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(Pagetitle))
            {
                ModelState.AddModelError("", "Content and Page title are required.");
                return View(post);
            }

            

            post.content = content;
            post.Pagetitle = Pagetitle;
            post.Auther = Auther ?? "Anonymous";
            post.heading = string.IsNullOrWhiteSpace(heading) ? Pagetitle : heading;

           
            if (image != null && image.Length > 0)
            {
                var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                post.ImageUrl = "/uploads/" + fileName;
            }

            try
            {
                _context.BlogPosts.Update(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating post: {ex.Message}");
                ModelState.AddModelError("", $"Failed to update post: {ex.Message}");
                return View(post); 
            }

            return RedirectToAction("OrderPosts");
        }
    }
    }

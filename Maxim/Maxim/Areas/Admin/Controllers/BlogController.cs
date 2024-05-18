using Maxim_Business.Exceptions.Blog;
using Maxim_Business.Services.Abstracts;
using Maxim_Core.Models;
using Microsoft.AspNetCore.Mvc;
using FileNotFoundException = Maxim_Business.Exceptions.Blog.FileNotFoundException;

namespace Maxim.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var blogs = _blogService.GetAllBlogs();
            return View(blogs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Blog blog)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _blogService.AddBlog(blog);
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(NullReferenceException ex)
            {
                return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var existBlog = _blogService.GetBlog(x => x.Id == id);
            if (existBlog == null) NotFound();

            try
            {
                _blogService.DeleteBlog(id);
            }
            catch(EntityNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(FileNotFoundException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var blog = _blogService.GetBlog(x =>x.Id == id);
            if (blog == null) return NotFound();

            return View(blog);
        }

        [HttpPost]
        
        public IActionResult Update(int id, Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                _blogService.UpdateBlog(id, blog);
            }
			catch (FileContentTypeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (FileSizeException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (EntityNotFoundException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (FileNotFoundException ex)
			{
				ModelState.AddModelError(ex.PropertyName, ex.Message);
				return View();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return RedirectToAction(nameof(Index));
		}
    }
}

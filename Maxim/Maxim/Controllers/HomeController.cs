
using Maxim_Business.Services.Abstracts;
using Maxim_Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Maxim.Controllers
{
    public class HomeController : Controller
    { 
        private readonly IBlogService _blogService;

		public HomeController(IBlogService blogService)
		{
			_blogService = blogService;
		}

		public IActionResult Index()
        {
            List<Blog> blogs = _blogService.GetAllBlogs();
            return View(blogs);
        }

    }
}

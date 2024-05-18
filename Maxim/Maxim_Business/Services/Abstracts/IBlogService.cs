using Maxim_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim_Business.Services.Abstracts
{
	public interface IBlogService
	{
		void AddBlog(Blog blog);
		void DeleteBlog(int id);
		void UpdateBlog(int id, Blog blog);
		Blog GetBlog(Func<Blog, bool>? func = null);
		List<Blog> GetAllBlogs(Func<Blog, bool>? func = null);
	}
}

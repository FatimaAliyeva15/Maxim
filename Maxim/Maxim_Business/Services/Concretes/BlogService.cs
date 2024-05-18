using Maxim_Business.Exceptions.Blog;
using Maxim_Business.Services.Abstracts;
using Maxim_Core.Models;
using Maxim_Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim_Business.Services.Concretes
{
	public class BlogService : IBlogService
	{
		private readonly IBlogRepository _blogRepository;

		public BlogService(IBlogRepository blogRepository)
		{
			_blogRepository = blogRepository;
		}

		public void AddBlog(Blog blog)
		{
			if (blog == null) throw new NullReferenceException("Doctor not found");
			if (!blog.ImgFile.ContentType.Contains("image/"))
				throw new FileContentTypeException("ImageFile", "File content type error");

			if (blog.ImgFile.Length > 2097152)
				throw new FileSizeException("ImageFile", "File size error");

			string fileName = blog.ImgFile.FileName;
			string path = @"C:\Users\Asus\source\repos\Maxim\Maxim\wwwroot\upload\blog\" + fileName;
			using(FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				blog.ImgFile.CopyTo(fileStream);
			}
			blog.ImgUrl = fileName;

			_blogRepository.Add(blog);
			_blogRepository.Commit();
		}

		public void DeleteBlog(int id)
		{
			var existBlog = _blogRepository.Get(x => x.Id == id);
			if (existBlog == null)
				throw new EntityNotFoundException("","Entity not found");

			string path = @"C:\Users\Asus\source\repos\Maxim\Maxim\wwwroot\upload\blog\" + existBlog.ImgUrl;
			if (!File.Exists(path))
				throw new Exceptions.Blog.FileNotFoundException("ImageFile", "File not found");

			File.Delete(path);

			_blogRepository.Delete(existBlog);
			_blogRepository.Commit();
		}

		public List<Blog> GetAllBlogs(Func<Blog, bool>? func = null)
		{
			return _blogRepository.GetAll(func);
		}

		public Blog GetBlog(Func<Blog, bool>? func = null)
		{
			return _blogRepository.Get(func);
		}

		public void UpdateBlog(int id, Blog blog)
		{
			var existBlog = _blogRepository.Get(x => x.Id == id);
			if (existBlog == null)
				throw new EntityNotFoundException("", "Entity not found");

			if (blog.ImgFile != null)
			{
				if (!blog.ImgFile.ContentType.Contains("image/"))
					throw new FileContentTypeException("ImageFile", "File content type error");

				if (blog.ImgFile.Length > 2097152)
					throw new FileSizeException("ImageFile", "File size error");

				string fileName = blog.ImgFile.FileName;
				string path = @"C:\Users\Asus\source\repos\Maxim\Maxim\wwwroot\upload\blog\" + fileName;
				using (FileStream fileStream = new FileStream(path, FileMode.Create))
				{
					blog.ImgFile.CopyTo(fileStream);
				}
				blog.ImgUrl = fileName;

				existBlog.ImgUrl = blog.ImgUrl;
			}
			existBlog.Name = blog.Name;
			existBlog.Description = blog.Description;

			_blogRepository.Commit();
		}
	}
}

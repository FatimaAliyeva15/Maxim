using Maxim_Core.Models;
using Maxim_Core.RepositoryAbstracts;
using Maxim_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim_Data.RepositoryConcretes
{
	public class BlogRepository : GenericRepository<Blog>, IBlogRepository
	{
		public BlogRepository(AppDbContext appDbContext) : base(appDbContext)
		{
		}
	}
}

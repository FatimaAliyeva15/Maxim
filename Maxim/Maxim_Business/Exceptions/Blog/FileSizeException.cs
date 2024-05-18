using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim_Business.Exceptions.Blog
{
	public class FileSizeException : Exception
	{
        public string PropertyName { get; set; }
        public FileSizeException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}

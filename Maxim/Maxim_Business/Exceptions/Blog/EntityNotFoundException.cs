
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxim_Business.Exceptions.Blog
{
	public class EntityNotFoundException : Exception
	{
        public string PropertyName { get; set; }
        public EntityNotFoundException(string propertyName, string? message) : base(message)
		{
			PropertyName = propertyName;
		}
	}
}
